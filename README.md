WebStack
==

A different look to webapp structure and development (in C#) ...

Every webapp is represented by a single handler (IRequestHandler). This handler is responsible for handling all requests to the application (at the appserver level, the application is registered to some url part, eg /app).

At the application level, the root IRequestHandler can be implemented in two ways. The first one is that handler directly provides some output (eg. file content or genereted HTML, XML etc), the second one is that handler dispatches request processing to the other handlers (eg. routing handler, exception processing handler, etc).

==

Read the tutorial on our **[WIKI](https://github.com/neptuo/WebStack/wiki)**.
