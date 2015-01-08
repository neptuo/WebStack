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
    public class FileSystemRequestHandler : RequestHandler
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

        protected override IHttpResponse TryHandle(IHttpRequest httpRequest)
        {
            string path = pathProvider.GetPath(httpRequest);
            if (!String.IsNullOrEmpty(path))
            {
                IReadOnlyFile file = rootDirectory.FindFiles(path, true).FirstOrDefault();
                if (file != null)
                    return BuildStreamResponse(file.GetContentAsStream(), new HttpMediaType("image/jpeg"));
            }

            return null;
        }
    }
}
