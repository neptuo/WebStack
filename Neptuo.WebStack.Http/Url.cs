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

        public string Host { get; set; }

        public string Path { get; set; }

        public string VirtualPath { get; set; }

        public bool HasSchema
        {
            get { return Schema != null; }
        }

        public bool HasHost
        {
            get { return Host != null; }
        }

        public bool HasPath
        {
            get { return Path != null; }
        }

        public bool HasVirtualPath
        {
            get { return VirtualPath != null; }
        }

        public static Url FromAbsolute(string schema, string host, string path)
        {
            Guard.NotNullOrEmpty(schema, "schema");
            Guard.NotNullOrEmpty(host, "host");
            Guard.NotNullOrEmpty(path, "path");
            return new Url(schema, host, path, null);
        }

        public static Url FromHost(string host, string path)
        {
            Guard.NotNullOrEmpty(host, "host");
            Guard.NotNullOrEmpty(path, "path");
            return new Url(null, host, path, null);
        }

        public static Url FromPath(string path)
        {
            return new Url(null, null, path, null);
        }

        public static Url FromVirtualPath(string virtualPath)
        {
            return new Url(null, null, null, virtualPath);
        }

        protected Url(string schema, string host, string path, string virtualPath)
        {
            Schema = schema;
            Host = host;
            Path = path;
            VirtualPath = virtualPath;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            if (HasSchema)
            {
                result.Append(Schema);
                result.Append(SchemaSeparator);
            }

            if (HasHost)
            {
                if (!HasSchema)
                    result.Append(NoSchemaPrefix);

                result.Append(Host);
            }

            if (HasPath)
                result.Append(Path);
            else
                result.Append(VirtualPath);

            return result.ToString();
        }

        public string ToString(string format)
        {
            Guard.NotNull(format, "format");

            StringBuilder result = new StringBuilder();
            bool hasSchema = false;
            bool hasHost = false;
            bool hasPath = false;
            foreach (char item in format)
            {
                if (item == 'S' && HasSchema && !hasSchema && !hasHost && !hasPath)
                {
                    result.Append(Schema);
                    result.Append(SchemaSeparator);
                    hasSchema = true;
                }
                else if (item == 'H' && HasHost && !hasHost && !hasPath)
                {
                    if (!hasSchema)
                        result.Append(NoSchemaPrefix);

                    result.Append(Host);
                    hasHost = true;
                }
                else if (item == 'P' && HasPath && !hasPath)
                {
                    result.Append(Path);
                }
                else
                {
                    throw Guard.Exception.NotSupported("'{0}' is not supported by URL formatter.", item);
                }
            }

            return result.ToString();
        }
    }
}
