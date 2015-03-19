using Neptuo.Activators;
using Neptuo.ComponentModel;
using Neptuo.FileSystems;
using Neptuo.Security.Cryptography;
using Neptuo.Templates.Compilation;
using Neptuo.Templates.Compilation.CodeGenerators;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Routing;
using Neptuo.WebStack.Templates.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Hosting
{
    public class TemplateRequestHandler : IRequestHandler
    {
        private readonly IViewService viewService;
        private readonly IReadOnlyFile templateFile;

        public TemplateRequestHandler(IReadOnlyFile templateFile)
            : this(Engine.Environment.WithViewService(), templateFile)
        { }

        public TemplateRequestHandler(IViewService viewService, IReadOnlyFile templateFile)
        {
            Ensure.NotNull(viewService, "viewService");
            Ensure.NotNull(templateFile, "templateFile");
            this.viewService = viewService;
            this.templateFile = templateFile;
        }

        public async Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            IReadOnlyFile templateFile;
            if (!httpContext.Request().RouteValues().TryGetTemplateFile(out templateFile))
                templateFile = this.templateFile;

            List<IErrorInfo> errors = new List<IErrorInfo>();
            ISourceContent sourceContent = new DefaultSourceContent(await templateFile.GetContentAsync());
            string className = String.Format("{0}_{1}", templateFile.Name, HashProvider.Sha1(sourceContent.TextContent));

            using (IDependencyContainer dependencyContainer = httpContext.DependencyProvider().Scope("TemplateCompilation"))
            {
                dependencyContainer
                    .Map<ICodeDomNaming>().InCurrentScope().To(new CodeDomDefaultNaming("Neptuo.WebStack.Templates", className));

                GeneratedView view = (GeneratedView)viewService.ProcessContent(
                    "Default",
                    sourceContent,
                    new DefaultViewServiceContext(
                        dependencyContainer,
                        errors
                    )
                );

                // Vyhodit vyjímku nebo rovnou vygenerovat chybovou stránku.
                if (view == null)
                    throw new TemplateCompilationException(errors, sourceContent);

                IComponentManager componentManager = new ComponentManager();
                IHtmlWriter htmlWriter = new HtmlTextWriter(httpContext.Response().OutputWriter());

                view.Init(dependencyContainer, componentManager);
                view.Render(componentManager, htmlWriter);
                view.Dispose();

                return true;
            }
        }
    }
}
