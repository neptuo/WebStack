using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    public class UrlBuilder : IUrlBuilder, IUrlHostBuilder, IUrlPathBuilder, IUrlQueryStringBuilder
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
        /// Matches query parameters.
        /// </summary>
        private static readonly Regex queryParser = new Regex(@"[?|&]([\w\.]+)=([^?|^&]+)");

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
        /// Current virtual path.
        /// </summary>
        private string virtualPath;

        /// <summary>
        /// Current query string.
        /// </summary>
        private Dictionary<string, string> queryString;

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public UrlBuilder(string applicationPath = null)
            : this(UrlBuilderSupportedPart.Schema | UrlBuilderSupportedPart.Host | UrlBuilderSupportedPart.Path | UrlBuilderSupportedPart.VirtualPath | UrlBuilderSupportedPart.QueryString, applicationPath)
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
        protected UrlBuilder(string schema, string host, string path, string virtualPath, Dictionary<string, string> queryString)
        {
            this.schema = schema;
            this.host = host;
            this.path = path;
            this.virtualPath = virtualPath;
            this.queryString = queryString;
        }

        public IReadOnlyUrl FromUrl(string url)
        {
            Ensure.NotNullOrEmpty(url, "url");

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
            string queryString;

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
                        queryString = url.Substring(path.Length);

                        Url result = Url.FromAbsolute(schema, host, path);
                        TrySetVirtualPath(result);
                        TrySetQueryParameters(result, queryString);
                        return result;
                    }

                    throw new UrlPartMalFormattedException("path", url);
                }
            }

            throw new UrlPartMalFormattedException("schema", url);
        }

        #region Schema

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
            Ensure.NotNullOrEmpty(schema, "schema");

            string output;
            if(!TrySchema(schema, out output) || output != schema)
                throw new UrlPartMalFormattedException("schema", schema);

            return new UrlBuilder(schema, null, null, null, null);
        }

        #endregion

        #region Host

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
            Ensure.NotNullOrEmpty(host, "host");

            string output;
            if(!TryHost(host, out output) || output != host)
                throw new UrlPartMalFormattedException("host", host);

            return new UrlBuilder(schema, host, null, null, null);
        }

        #endregion

        #region Path

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

        public IUrlQueryStringBuilder Path(string path)
        {
            Ensure.NotNullOrEmpty(path, "path");

            string output;
            if (!TryPath(path, out output) || output != path)
                throw new UrlPartMalFormattedException("path", path);

            return new UrlBuilder(schema, host, path, null, null);
        }

        #endregion

        #region VirtualPath

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

        public IUrlQueryStringBuilder VirtualPath(string virtualPath)
        {
            Ensure.NotNullOrEmpty(virtualPath, "virtualPath");
            if (virtualPath[0] != '~' || virtualPath[1] != '/')
                throw Ensure.Exception.ArgumentOutOfRange("path", "Path argument must start with '~/'.");

            string output;
            if (!TryVirtualPath(virtualPath, out output) || output != virtualPath)
                throw new UrlPartMalFormattedException("virtualPath", virtualPath);

            Url url = Url.FromVirtualPath(virtualPath);
            TrySetPath(url);

            return new UrlBuilder(schema, host, null, virtualPath, null);
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

        #endregion

        #region QueryString

        /// <summary>
        /// Tries to make <paramref name="key"/> valid.
        /// </summary>
        /// <param name="key">Source key.</param>
        /// <param name="targetKey">Target valid key.</param>
        /// <returns><c>true</c> if validation was successfull; <c>false</c> otherwise.</returns>
        protected bool TryParameterKey(string key, out string targetKey)
        {
            targetKey = key;
            return true;
        }

        /// <summary>
        /// Tries to make <paramref name="value"/> valid.
        /// </summary>
        /// <param name="value">Source value.</param>
        /// <param name="targetValue">Target valid value.</param>
        /// <returns><c>true</c> if validation was successfull; <c>false</c> otherwise.</returns>
        protected bool TryParameterValue(string value, out string targetValue)
        {
            targetValue = value;
            return true;
        }

        /// <summary>
        /// Tries to set query string parameters from <paramref name="queryString"/> to <paramref name="url"/>.
        /// </summary>
        /// <param name="url">Target URL.</param>
        /// <param name="queryString">Source query string.</param>
        /// <returns><c>true</c> if validation was successfull; <c>false</c> otherwise.</returns>
        private bool TrySetQueryParameters(Url url, string queryString)
        {
            Match match = queryParser.Match(queryString);
            while (match.Success)
            {
                url.QueryStringKey(match.Groups[1].Value, match.Groups[2].Value);
                match = match.NextMatch();
            }

            return true;
        }

        public IUrlQueryStringBuilder Parameter(string key, string value)
        {
            Ensure.NotNullOrEmpty(key, "key");
            Ensure.NotNull(value, "value");

            string targetKey;
            if (!TryParameterKey(key, out targetKey))
                throw new UrlPartMalFormattedException("queryString.key", key);

            string targetValue;
            if (!TryParameterValue(value, out targetValue))
                throw new UrlPartMalFormattedException("queryString.value", value);

            Dictionary<string, string> newQueryString;
            if (queryString != null)
                newQueryString = new Dictionary<string, string>(queryString);
            else
                newQueryString = new Dictionary<string, string>();

            newQueryString[targetKey] = targetValue;
            return new UrlBuilder(schema, host, path, virtualPath, newQueryString);
        }

        public IReadOnlyUrl ToUrl()
        {
            Url url;
            if (!String.IsNullOrEmpty(virtualPath))
            {
                url = Url.FromVirtualPath(virtualPath);
                TrySetPath(url);
            }
            else
            {
                url = Url.FromAbsolute(schema, host, path);
                TrySetVirtualPath(url);
            }

            if (queryString != null)
            {
                foreach (KeyValuePair<string, string> parameter in queryString)
                    url.QueryStringKey(parameter.Key, parameter.Value);
            }

            return url;
        }

        #endregion
    }
}
