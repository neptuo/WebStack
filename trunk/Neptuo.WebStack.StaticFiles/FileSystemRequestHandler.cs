using Neptuo.FileSystems;
using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.StaticFiles
{
    public class FileSystemRequestHandler : IRequestHandler
    {
        private readonly IReadOnlyDirectory rootDirectory;
        private readonly IPathProvider pathProvider;

        public FileSystemRequestHandler(IReadOnlyDirectory rootDirectory, IPathProvider pathProvider)
        {
            Guard.NotNull(rootDirectory, "rootDirectory");
            Guard.NotNull(pathProvider, "pathProvider");
            this.rootDirectory = rootDirectory;
            this.pathProvider = pathProvider;
        }

        public Task<bool> TryHandleAsync(IHttpContext httpContext)
        {
            string path = pathProvider.GetPath(httpContext);
            if (!String.IsNullOrEmpty(path))
            {
                IReadOnlyFile file = rootDirectory.FindFiles(path, true).FirstOrDefault();
                if (file != null)
                {
                    using (Stream fileContent = file.GetContentAsStream())
                    {
                        fileContent.CopyTo(httpContext.Response().OutputStream());
                    }

                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }
    }
}
