WebStack
========

A different look to webapp structure and development (in C#) ...

Every webapp is represented by a single handler (IRequestHandler). This handler is responsible for handling all requests to the application (at the appserver level, the application is registered to some url part, eg /app).

At the application level, the root IRequestHandler can be implemented in two ways. The first one is that handler directly provides some output (eg. file content or genereted HTML, XML etc), the second one is that handler dispatches request processing to the other handlers (eg. routing handler, exception processing handler, etc).

---------

Request handler (represented by the interface IRequestHandler) defines single method for handling HTTP request and providing HTTP response. This method has the following signature:

Task<IHttpResponse> TryHandleAsync(IHttpRequest httpRequest)

Returning any not null response from request handler means that the handler was able to process the incoming request   and calling client should receive this response. If null is provided by the handler, it means that handler was not able to handle the incoming request and next handler in the pipeline should be called.

This behavior provides ability to compose handlers in the processing pipeline. Also practically any handler can be reused in composition level. Example pipeline described in the following paragraphs is composited from exception handler, routing handler and "delegating handler". These building blocks are describes at the first. 

ExceptionRequestHandler
This implementation of IRequestHandler takes next request handler in the constructor and any call to TryHandleAsync delegates to this inner request handler. Besides this processing delegation, it wraps calling of the TryHandleAsync of the inner handler witch try/catch block and handles all raised exceptions. It also provides mechanism to delegate processing concrete exceptions to the "exception handler" which gets instance of the concrete exception and should provide some better error page for this exception. So this handler is ideal as top level handler for catching any exception that can occur while processing incoming request and providing error pages.

RoutingRequestHandler
With experiences how the ExceptionRequestHandler works, it's quite obvious what routing handler is doing. It creates routing table (it's better to say "routing tree") and each registered URL is registered with the concrete handler to process requests to this URL. If the route table doesn't contain row to match current request, routing handler simple returns null to say "I'm not able to handle this request".

DelegatingRequestHandler
The last handler to mention here is the "delegating" one. This handler takes collection of inner handlers and calls each until finds one, that doesn't returned null. So "First not null response is taken".

Described handlers give us base processing pipeline:

  - ExceptionRequestHandler
    - DelegatingRequetHandler
      - RoutingRequestHandler
        - … (app specific routing table)
      - NotFoundRequestHandler (404 page provider)

When processing pipeline is configured this way, all requests go through the exception handler, so all exceptions are caught (and logged) and error page is generated if needed (each exception can be processed by registered "concrete exception handler"). Then processing of requests go to the routing handler, where routing table is searched for registered handler for concrete URL, if such a handler is found, processing is delegated to this handler, otherwise routing handler returns null and "not found" handler is executed for providing error page. 

Oh, wait a moment, where is the handler for static files? Like in OWIN apps, this processing pipeline should be used for all requets, so somewhere should be registered handler for files from file system. This one can placed to the delegating handler and it's app you if to place it before routing handler (so static files takes precedence over URLs registered to the routing table) or after routing handler (so, adding URL to the routing table can override static file). And surely, static file system handler can also be placed to the routing table (even multiple times with different root directories).


This way request handlers are registered as singletons, if need to create handler instance for each request, simply by using the composite pattern, we create request handler that works like a factory for passed request handler type.

Features to reimplement:
	- IReadOnlyUrl MUST contains query string parameters.
	- RoutingRequestHandler SHOULD support query string, HTTP method, header values.

