using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    public interface IUrlBuilder
    {
        IReadOnlyUrl FromUrl(string url);

        IUrlDomainBuilder Schema(string schema);
        IUrlPathBuilder Domain(string domain);
        IReadOnlyUrl Path(string path);
        IReadOnlyUrl VirtualPath(string virtualPath);
    }

    public interface IUrlDomainBuilder
    {
        IUrlPathBuilder Domain(string domain);
    }

    public interface IUrlPathBuilder
    {
        IReadOnlyUrl Path(string path);
    }


    public class UrlBuilder : IUrlBuilder, IUrlDomainBuilder, IUrlPathBuilder
    {
        private static readonly Regex domainParser = new Regex("([a-zA-Z0-9]+).(([a-zA-Z0-9]+).([a-zA-Z0-9]+))");

        private string schema;
        private string domain;
        private string path;

        protected UrlBuilder(string schema, string domain, string path)
        {
            this.schema = schema;
            this.domain = domain;
            this.path = path;
        }

        public IReadOnlyUrl FromUrl(string url)
        {
            throw Guard.Exception.NotImplemented();
        }

        public IUrlDomainBuilder Schema(string schema)
        {
            Guard.NotNullOrEmpty(schema, "schema");
            return new UrlBuilder(schema, null, null);
        }

        public IUrlPathBuilder Domain(string domain)
        {
            Guard.NotNullOrEmpty(domain, "domain");
            return new UrlBuilder(schema, domain, null);
        }

        public IReadOnlyUrl Path(string path)
        {
            Guard.NotNullOrEmpty(path, "path");
            if (path[0] != '/')
                throw Guard.Exception.ArgumentOutOfRange("path", "Path argument must start with '/'.");

            if (schema != null)
                return new Url(schema, domain, path);

            if (domain != null)
                return new Url(domain, path);

            return new Url(path);
        }

        public IReadOnlyUrl VirtualPath(string virtualPath)
        {
            Guard.NotNullOrEmpty(path, "path");
            if (path[0] != '~' || path[1] != '/')
                throw Guard.Exception.ArgumentOutOfRange("path", "Path argument must start with '~/'.");

            return new Url(virtualPath);
        }
    }
}
