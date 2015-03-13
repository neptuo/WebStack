using Neptuo.FileSystems;
using Neptuo.WebStack.Http;
using Neptuo.WebStack.Http.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.StaticFiles
{
    public class FileSystemRequestHandler : RequestHandler
    {
        private readonly IReadOnlyDirectory rootDirectory;
        private readonly IPathProvider pathProvider;

        public FileSystemRequestHandler(IReadOnlyDirectory rootDirectory, IPathProvider pathProvider)
        {
            Ensure.NotNull(rootDirectory, "rootDirectory");
            Ensure.NotNull(pathProvider, "pathProvider");
            this.rootDirectory = rootDirectory;
            this.pathProvider = pathProvider;
        }

        protected override bool TryHandle(IHttpContext httpContext)
        {
            string path = pathProvider.GetPath(httpContext);
            if (!String.IsNullOrEmpty(path))
            {
                IReadOnlyFile file = rootDirectory.FindFiles(path, true).FirstOrDefault();
                if (file != null)
                {
                    file.GetContentAsStream().CopyTo(httpContext.ResponseMessage().BodyStream);
                    httpContext.Response().Headers().ContentType(new HttpMediaType("image/jpeg"));
                    return true;
                }
            }

            return false;
        }
    }
}
