using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    public class Url : IReadOnlyUrl
    {
        public const string ProtocolSeparator = "://";
        public const string NoProtocolPrefix = "//";
        public const string VirtualPathPrefix = "~/";
        public const string PathPrefix = "/";

        public string Schema { get; set; }

        public string Domain { get; set; }

        public string Path { get; set; }

        public bool HasSchema
        {
            get { return Schema != null; }
        }

        public bool HasDomain
        {
            get { return Domain != null; }
        }

        public bool HasVirtualPath
        {
            get { return Path.StartsWith(VirtualPathPrefix); }
        }

        public string VirtualPath
        {
            get
            {
                string path = Path;
                if(!HasVirtualPath)
                {
                    if (path.StartsWith("/"))
                        path = VirtualPathPrefix + path.Substring(1);
                    else
                        path = VirtualPathPrefix + path;
                }

                return path;
            }
        }

        internal Url()
        { }

        public Url(string url)
        {
            Guard.NotNullOrEmpty(url, "url");

            if(url.StartsWith(VirtualPathPrefix))
            {
                ParsePath(url);
            }
            else if (url.StartsWith(NoProtocolPrefix))
            {
                url = url.Substring(NoProtocolPrefix.Length);
                url = ParseDomain(url);
                ParsePath(url);
            }
            else
            {
                url = ParseProtocol(url);
                url = ParseDomain(url);
                ParsePath(url);
            }
        }

        public Url(string schema, string domain, string path)
        {
            Guard.NotNullOrEmpty(schema, "schema");
            Guard.NotNullOrEmpty(domain, "domain");
            Guard.NotNullOrEmpty(path, "path");
            Schema = schema;
            Domain = domain;
            Path = path;
        }

        public Url(string domain, string path)
        {
            Guard.NotNullOrEmpty(domain, "domain");
            Guard.NotNullOrEmpty(path, "path");
            Domain = domain;
            Path = path;
        }

        public static implicit operator Url(string url)
        {
            return new Url(url);
        }

        private string ParseProtocol(string url)
        {
            int indexOfProtocolSeparator = url.IndexOf(ProtocolSeparator);
            if(indexOfProtocolSeparator > 0)

            Schema = url.Substring(0, indexOfProtocolSeparator);

            url = url.Substring(indexOfProtocolSeparator + ProtocolSeparator.Length);
            return url;
        }

        private string ParseDomain(string url)
        {
            int indexOfSlash = url.IndexOf(PathPrefix);
            Domain = url.Substring(0, indexOfSlash);

            url = url.Substring(indexOfSlash);
            return url;
        }

        private void ParsePath(string url)
        {
            if (!url.StartsWith(PathPrefix) && !url.StartsWith(VirtualPathPrefix))
                throw new Exception();

            Path = url;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (HasSchema)
            {
                result.Append(Schema);
                result.Append(ProtocolSeparator);
            }

            if (HasDomain)
            {
                if (!HasSchema)
                    result.Append(NoProtocolPrefix);

                result.Append(Domain);
            }

            result.Append(Path);
            return result.ToString();
        }
    }
}
