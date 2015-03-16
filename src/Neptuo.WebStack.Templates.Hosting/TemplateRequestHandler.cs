using Neptuo.Activators;
using Neptuo.ComponentModel;
using Neptuo.FileSystems;
using Neptuo.Templates.Compilation;
using Neptuo.Templates.Compilation.CodeGenerators;
using Neptuo.WebStack.Http;
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

        public TemplateRequestHandler(IViewService viewService, IReadOnlyFile templateFile)
        {
            Ensure.NotNull(viewService, "viewService");
            Ensure.NotNull(templateFile, "templateFile");
            this.viewService = viewService;
            this.templateFile = templateFile;
        }

        public async Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            List<IErrorInfo> errors = new List<IErrorInfo>();
            ISourceContent sourceContent = new DefaultSourceContent(await templateFile.GetContentAsync());

            using (IDependencyContainer dependencyContainer = httpContext.DependencyProvider().Scope("TemplateCompilation"))
            {
                dependencyContainer
                    .Map<ICodeDomNaming>().InCurrentScope().To(new CodeDomDefaultNaming("Neptuo.WebStack.Templates", templateFile.Name + DateTime.Now.ToFileTime()));

                GeneratedView view = (GeneratedView)viewService.ProcessContent(
                    "Default",
                    sourceContent,
                    new DefaultViewServiceContext(
                        dependencyContainer,
                        errors
                    )
                );

                if (view == null)
                    throw new TemplateCompilationException(errors, sourceContent);

                IComponentManager componentManager = new ComponentManager();
                IHtmlWriter htmlWriter = new HtmlTextWriter(httpContext.Response().OutputWriter());

                view.Init(dependencyContainer, componentManager);
                view.OnInit(componentManager);
                view.Render(htmlWriter);
                view.Dispose();

                return true;
            }
        }
    }
}
