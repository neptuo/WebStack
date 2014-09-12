using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing
{
    /// <summary>
    /// Contains single route URL pattern.
    /// Support protocols, domains, app-relative URLs etc.
    /// </summary>
    public class RoutePattern
    {
        public const string ProtocolSeparator = "://";
        public const string NoProtocolPrefix = "//";
        public const string VirtualPathPrefix = "~/";
        public const string PathPrefix = "/";

        public string Protocol { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }

        public bool HasProtocol
        {
            get { return Protocol != null; }
        }

        public bool HasDomain
        {
            get { return Domain != null; }
        }

        public bool IsVirtualPath
        {
            get { return Path.StartsWith(VirtualPathPrefix); }
        }

        public RoutePattern(string url)
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

        public RoutePattern(string protocol, string domain, string path)
        {
            Guard.NotNullOrEmpty(protocol, "protocol");
            Guard.NotNullOrEmpty(domain, "domain");
            Guard.NotNullOrEmpty(path, "path");
            Protocol = protocol;
            Domain = domain;
            Path = path;
        }

        public RoutePattern(string domain, string path)
        {
            Guard.NotNullOrEmpty(domain, "domain");
            Guard.NotNullOrEmpty(path, "path");
            Domain = domain;
            Path = path;
        }

        public static implicit operator RoutePattern(string url)
        {
            return new RoutePattern(url);
        }

        private string ParseProtocol(string url)
        {
            int indexOfProtocolSeparator = url.IndexOf(ProtocolSeparator);
            Protocol = url.Substring(0, indexOfProtocolSeparator);

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
            if (HasProtocol)
            {
                result.Append(Protocol);
                result.Append(ProtocolSeparator);
            }

            if (HasDomain)
            {
                if (!HasProtocol)
                    result.Append(NoProtocolPrefix);

                result.Append(Domain);
            }

            result.Append(Path);
            return result.ToString();
        }
    }
}
