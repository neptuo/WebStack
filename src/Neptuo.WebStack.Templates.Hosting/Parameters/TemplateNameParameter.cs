using Neptuo.FileSystems;
using Neptuo.WebStack.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Hosting.Parameters
{
    public class TemplateNameParameter : IRouteParameter
    {
        private readonly IReadOnlyDirectory viewRootDirectory;
        private readonly string urlSuffix;
        private readonly string fileSuffix;

        public TemplateNameParameter(IReadOnlyDirectory viewRootDirectory, string urlSuffix, string fileSuffix)
        {
            Ensure.NotNull(viewRootDirectory, "viewRootDirectory");
            this.viewRootDirectory = viewRootDirectory;
            this.urlSuffix = urlSuffix;
            this.fileSuffix = fileSuffix;
        }

        public bool TryMatchUrl(IRouteParameterMatchContext context)
        {
            string filePart = null;
            string urlPart = context.RemainingUrl;
            if (!String.IsNullOrEmpty(urlSuffix))
            {
                if(!urlPart.EndsWith(urlSuffix))
                    return false;

                urlPart = urlPart.Substring(0, urlPart.Length - urlSuffix.Length);
            }
            else
            {
                if (Path.HasExtension(urlPart))
                    urlPart = urlPart.Substring(0, urlPart.Length - Path.GetExtension(urlPart).Length);
            }

            filePart = urlPart;
            if (!String.IsNullOrEmpty(fileSuffix))
                filePart = filePart + fileSuffix;

            IReadOnlyFile templateFile = viewRootDirectory.FindFiles(filePart, true).FirstOrDefault();
            if (templateFile == null)
                return false;

            context.RouteValues.TemplateFile(templateFile);
            context.RemainingUrl = context.RemainingUrl.Substring(urlPart.Length);
            return true;
        }
    }
}
