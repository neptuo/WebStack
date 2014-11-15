﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    public class UrlBuilder : IUrlBuilder, IUrlHostBuilder, IUrlPathBuilder
    {
        /// <summary>
        /// Matches protocol name.
        /// </summary>
        private static readonly Regex schemaParser = new Regex(@"^(?<protocol>\w+)://");

        /// <summary>
        /// Matches domain name.
        /// </summary>
        private static readonly Regex hostParser = new Regex(@"^(?<host>\w+)[^/]+");

        /// <summary>
        /// Matches path.
        /// </summary>
        private static readonly Regex pathParser = new Regex(@"^(?<path>/\w*)[^?]*");

        /// <summary>
        /// List of supported root parts.
        /// </summary>
        private readonly UrlBuilderSupportedPart supportedPart;

        /// <summary>
        /// Path for virtual path rewrite.
        /// </summary>
        private readonly string applicationPath;

        /// <summary>
        /// Current schema name.
        /// </summary>
        private string schema;

        /// <summary>
        /// Current domain name + port.
        /// </summary>
        private string host;

        /// <summary>
        /// Current path.
        /// </summary>
        private string path;

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public UrlBuilder(string applicationPath = null)
            : this(UrlBuilderSupportedPart.Schema | UrlBuilderSupportedPart.Host | UrlBuilderSupportedPart.Path | UrlBuilderSupportedPart.VirtualPath, applicationPath)
        { }

        /// <summary>
        /// Creates new empty instance with support for root URL starting with <paramref name="supportPart"/>.
        /// </summary>
        /// <param name="supportedPart">List supported URL parts for this builder.</param>
        /// <param name="applicationPath">Path for virtual path rewrite.</param>
        public UrlBuilder(UrlBuilderSupportedPart supportedPart, string applicationPath = null)
        {
            this.supportedPart = supportedPart;
            this.applicationPath = applicationPath;
        }

        /// <summary>
        /// Used is fluently returning methods.
        /// </summary>
        /// <param name="schema">Current schema name.</param>
        /// <param name="host">Current domain name + port.</param>
        /// <param name="path">Current path.</param>
        protected UrlBuilder(string schema, string host, string path)
        {
            this.schema = schema;
            this.host = host;
            this.path = path;
        }

        public IReadOnlyUrl FromUrl(string url)
        {
            Guard.NotNullOrEmpty(url, "url");

            string virtualPath;
            if (TryVirtualPath(url, out virtualPath))
            {
                Url result = Url.FromVirtualPath(virtualPath);
                TrySetPath(result);
                return result;
            }

            string schema;
            string host;
            string path;

            if (url.StartsWith(Url.NoSchemaPrefix))
            {
                url = url.Substring(2);
                if (TryHost(url, out host))
                {
                    url = url.Substring(host.Length);
                    if (TryPath(url, out path))
                    {
                        Url result = Url.FromHost(host, path);
                        TrySetVirtualPath(result);
                        return result;
                    }

                    throw new UrlPartMalFormattedException("path", url);
                }

                throw new UrlPartMalFormattedException("host", url);
            }

            if (TryPath(url, out path))
                return Url.FromPath(path);

            if (TrySchema(url, out schema))
            {
                url = url.Substring(schema.Length + Url.SchemaSeparator.Length);
                if (TryHost(url, out host))
                {
                    url = url.Substring(host.Length);
                    if (TryPath(url, out path))
                    {
                        Url result = Url.FromAbsolute(schema, host, path);
                        TrySetVirtualPath(result);
                        return result;
                    }

                    throw new UrlPartMalFormattedException("path", url);
                }
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

        public IUrlHostBuilder Schema(string schema)
        {
            Guard.NotNullOrEmpty(schema, "schema");

            string output;
            if(!TrySchema(schema, out output) || output != schema)
                throw new UrlPartMalFormattedException("schema", schema);

            return new UrlBuilder(schema, null, null);
        }

        protected bool TryHost(string url, out string host)
        {
            Match match = hostParser.Match(url);
            if (!match.Success || match.Index != 0)
            {
                host = null;
                return false;
            }

            host = url.Substring(0, match.Length);
            return true;
        }

        public IUrlPathBuilder Host(string host)
        {
            Guard.NotNullOrEmpty(host, "host");

            string output;
            if(!TryHost(host, out output) || output != host)
                throw new UrlPartMalFormattedException("host", host);

            return new UrlBuilder(schema, host, null);
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
                return Url.FromAbsolute(schema, host, path);

            if (host != null)
                return Url.FromHost(host, path);

            Url url = Url.FromPath(path);
            TrySetVirtualPath(url);
            return url;
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

            Url url = Url.FromVirtualPath(virtualPath);
            TrySetPath(url);
            return url;
        }

        /// <summary>
        /// Tries to set <see cref="Url.VirtualPath"/> from <see cref="applicationPath"/> and <see cref="Url.Path"/>.
        /// </summary>
        /// <param name="url">The URL to update virtual path on.</param>
        private void TrySetVirtualPath(Url url)
        {
            if (applicationPath != null && url.Path.StartsWith(applicationPath))
            {
                string part = url.Path.Substring(applicationPath.Length);
                if(!String.IsNullOrEmpty(part) && part[0] == '/')
                    part = part.Substring(1);

                url.VirtualPath = Url.VirtualPathPrefix + part;
            }
        }

        /// <summary>
        /// Tries to set <see cref="Url.Path"/> from <see cref="applicationPath"/> and <see cref="Url.VirtualPath"/>.
        /// </summary>
        /// <param name="url">The URL to update path on.</param>
        private void TrySetPath(Url url)
        {
            if (applicationPath != null)
            {
                if (applicationPath == "/")
                    url.Path = url.VirtualPath.Substring(1);
                else
                    url.Path = url.VirtualPath.Replace("~", applicationPath);
            }
        }
    }
}
