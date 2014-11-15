using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Builder for <see cref="IReadOnlyUrl"/>.
    /// </summary>
    public interface IUrlBuilder : IUrlDomainBuilder, IUrlPathBuilder
    {
        /// <summary>
        /// Parses <paramref name="url"/> info <see cref="IReadOnlyUrl"/>.
        /// </summary>
        /// <param name="url">String representation of URL.</param>
        /// <returns></returns>
        IReadOnlyUrl FromUrl(string url);

        /// <summary>
        /// Sets schema part of URL in the current builder.
        /// </summary>
        /// <param name="schema">Schema name.</param>
        /// <returns>Domain name builder with schema set to <paramref name="schema"/>.</returns>
        IUrlDomainBuilder Schema(string schema);

        /// <summary>
        /// Creates virtual URL (starts with '~/').
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <returns>Built URL.</returns>
        IReadOnlyUrl VirtualPath(string virtualPath);
    }

    /// <summary>
    /// Part of <see cref="IUrlBuilder"/> for building domain name of the URL.
    /// </summary>
    public interface IUrlDomainBuilder
    {
        /// <summary>
        /// Sets domain name part of URL in the current builder.
        /// </summary>
        /// <param name="domain">Domain name.</param>
        /// <returns>Path builder with domain name set to <paramref name="domain"/>.</returns>
        IUrlPathBuilder Domain(string domain);
    }

    /// <summary>
    /// Part of <see cref="IUrlBuilder"/> for path of the URL.
    /// </summary>
    public interface IUrlPathBuilder
    {
        /// <summary>
        /// Sets path part of URL in the current builder.
        /// </summary>
        /// <param name="path">Path string.</param>
        /// <returns>Built URL.</returns>
        IReadOnlyUrl Path(string path);
    }


    public class UrlBuilder : IUrlBuilder, IUrlDomainBuilder, IUrlPathBuilder
    {
        /// <summary>
        /// Matches protocol name.
        /// </summary>
        private static readonly Regex schemaParser = new Regex(@"^(?<protocol>\w+)://");

        /// <summary>
        /// Matches domain name.
        /// </summary>
        private static readonly Regex domainParser = new Regex(@"^(?<domain>\w+)[^/]+");

        /// <summary>
        /// Matches path.
        /// </summary>
        private static readonly Regex pathParser = new Regex(@"^(?<path>/\w*)[^?]*");

        /// <summary>
        /// Current schema name.
        /// </summary>
        private string schema;

        /// <summary>
        /// Current domain name.
        /// </summary>
        private string domain;

        /// <summary>
        /// Current path.
        /// </summary>
        private string path;

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public UrlBuilder()
        { }

        /// <summary>
        /// Used is fluently returning methods.
        /// </summary>
        /// <param name="schema">Current schema name.</param>
        /// <param name="domain">Current domain name.</param>
        /// <param name="path">Current path.</param>
        protected UrlBuilder(string schema, string domain, string path)
        {
            this.schema = schema;
            this.domain = domain;
            this.path = path;
        }

        public IReadOnlyUrl FromUrl(string url)
        {
            Guard.NotNullOrEmpty(url, "url");

            string virtualPath;
            if(TryVirtualPath(url, out virtualPath))
                return Url.FromVirtualPath(virtualPath);

            string path;
            if (TryPath(url, out path))
                return Url.FromPath(path);

            string schema;
            string domain;

            if (TrySchema(url, out schema))
            {
                url = url.Substring(schema.Length + Url.SchemaSeparator.Length);
                if (TryDomain(url, out domain))
                {
                    url = url.Substring(domain.Length);
                    if (TryPath(url, out path))
                        return Url.FromAbsolute(schema, domain, path);

                    throw new UrlPartMalFormattedException("path", url);
                }
            }

            if (url.StartsWith(Url.NoSchemaPrefix))
            {
                url = url.Substring(2);
                if (TryDomain(url, out domain))
                {
                    url = url.Substring(domain.Length);
                    if (TryPath(url, out path))
                        return Url.FromDomain(domain, path);

                    throw new UrlPartMalFormattedException("path", url);
                }

                throw new UrlPartMalFormattedException("domain", url);
            }

            throw new UrlPartMalFormattedException("schema", url);
        }

        protected bool TrySchema(string url, out string schema)
        {
            Match match = schemaParser.Match(url);
            if (!match.Success || match.Index != 0)
            {
                schema = null;
                return false;
            }

            schema = match.Groups["protocol"].Value;
            return true;
        }

        public IUrlDomainBuilder Schema(string schema)
        {
            Guard.NotNullOrEmpty(schema, "schema");

            string output;
            if(!TrySchema(schema, out output) || output != schema)
                throw new UrlPartMalFormattedException("schema", schema);

            return new UrlBuilder(schema, null, null);
        }

        protected bool TryDomain(string url, out string domain)
        {
            Match match = domainParser.Match(url);
            if (!match.Success || match.Index != 0)
            {
                domain = null;
                return false;
            }

            domain = url.Substring(0, match.Length);
            return true;
        }

        public IUrlPathBuilder Domain(string domain)
        {
            Guard.NotNullOrEmpty(domain, "domain");

            string output;
            if(!TryDomain(domain, out output) || output != domain)
                throw new UrlPartMalFormattedException("domain", domain);

            return new UrlBuilder(schema, domain, null);
        }

        protected bool TryPath(string url, out string path)
        {
            Match match = pathParser.Match(url);
            if (!match.Success || match.Index != 0)
            {
                path = null;
                return false;
            }

            path = url.Substring(0, match.Length);
            return true;
        }

        public IReadOnlyUrl Path(string path)
        {
            Guard.NotNullOrEmpty(path, "path");

            string output;
            if(!TryPath(path, out output) || output != path)
                throw new UrlPartMalFormattedException("path", path);

            if (schema != null)
                return Url.FromAbsolute(schema, domain, path);

            if (domain != null)
                return Url.FromDomain(domain, path);

            return Url.FromPath(path);
        }

        protected bool TryVirtualPath(string url, out string virtualPath)
        {
            if (url.StartsWith(Url.VirtualPathPrefix))
            {
                url = url.Substring(1);
                Match match = pathParser.Match(url);
                if (!match.Success || match.Index != 0)
                {
                    virtualPath = null;
                    return false;
                }

                virtualPath = Url.VirtualPathPrefix + url.Substring(1, match.Length - 1);
                return true;
            }

            virtualPath = null;
            return false;
        }

        public IReadOnlyUrl VirtualPath(string virtualPath)
        {
            Guard.NotNullOrEmpty(path, "path");
            if (path[0] != '~' || path[1] != '/')
                throw Guard.Exception.ArgumentOutOfRange("path", "Path argument must start with '~/'.");

            string output;
            if(!TryVirtualPath(virtualPath, out output) || output != virtualPath)
                throw new UrlPartMalFormattedException("virtualPath", virtualPath);
            
            return Url.FromVirtualPath(virtualPath);
        }
    }
}
