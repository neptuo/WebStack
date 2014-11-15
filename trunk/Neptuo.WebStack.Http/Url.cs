using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    internal class Url : IReadOnlyUrl
    {
        public const string SchemaSeparator = "://";
        public const string NoSchemaPrefix = "//";
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

        public static Url FromAbsolute(string schema, string domain, string path)
        {
            Guard.NotNullOrEmpty(schema, "schema");
            Guard.NotNullOrEmpty(domain, "domain");
            Guard.NotNullOrEmpty(path, "path");
            return new Url(schema, domain, path);
        }

        public static Url FromDomain(string domain, string path)
        {
            Guard.NotNullOrEmpty(domain, "domain");
            Guard.NotNullOrEmpty(path, "path");
            return new Url(null, domain, path);
        }

        public static Url FromPath(string path)
        {
            return new Url(null, null, path);
        }

        public static Url FromVirtualPath(string virtualPath)
        {
            return new Url(null, null, virtualPath);
        }

        protected Url(string schema, string domain, string path)
        {
            Schema = schema;
            Domain = domain;
            Path = path;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            if (HasVirtualPath)
            {
                result.Append(VirtualPathPrefix);
                result.Append(Path.Substring(1));
            }
            else
            {
                if (HasSchema)
                {
                    result.Append(Schema);
                    result.Append(SchemaSeparator);
                }

                if (HasDomain)
                {
                    if (!HasSchema)
                        result.Append(NoSchemaPrefix);

                    result.Append(Domain);
                }

                result.Append(Path);
            }

            return result.ToString();
        }
    }
}
