using Neptuo.WebStack.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Routing.Segments
{
    public class StaticRouteSegment : RouteSegment
    {
        public IPipelineFactory PipelineFactory { get; private set; }

        public StaticRouteSegment(string urlPart)
        {
            Children = new List<RouteSegment>();
            UrlPart = urlPart;
        }

        public StaticRouteSegment(string urlPart, IPipelineFactory pipelineFactory)
            : this(urlPart)
        {
            PipelineFactory = pipelineFactory;
        }

        private string TryMatchUrlPart(string url)
        {
            if (url.StartsWith(UrlPart))
            {
                url = url.Substring(UrlPart.Length);
                return url;
            }
            return null;
        }

        public override bool TryMatchUrl(string url, out IPipelineFactory pipelineFactory)
        {
            url = TryMatchUrlPart(url);
            if (url != null)
            {
                if (url.Length != 0)
                {
                    foreach (RouteSegment routeSegment in Children)
                    {
                        if (routeSegment.TryMatchUrl(url, out pipelineFactory))
                            return true;
                    }
                }
                else if (PipelineFactory != null)
                {
                    pipelineFactory = PipelineFactory;
                    return true;
                }
            }

            pipelineFactory = null;
            return false;
        }

        public override void IncludeUrl(string url, IPipelineFactory pipelineFactory)
        {
            url = TryMatchUrlPart(url);
            if (url != null)
            {
                if (url.Length == 0)
                {
                    PipelineFactory = pipelineFactory;
                    return;
                }

                foreach (RouteSegment routeSegment in Children.ToList())
                {
                    int index = 0;
                    while (IsCharAtEquals(routeSegment.UrlPart, url, index))
                    {
                        index++;
                    }

                    if(index == routeSegment.UrlPart.Length)
                    {
                        routeSegment.IncludeUrl(url, pipelineFactory);
                        return;
                    }

                    if (index > 0)
                    {
                        // Divide
                        Children.Remove(routeSegment);
                        StaticRouteSegment newRouteSegment = new StaticRouteSegment(routeSegment.UrlPart.Substring(0, index));
                        Children.Add(newRouteSegment);
                        newRouteSegment.Children.Add(routeSegment);

                        routeSegment.UrlPart = routeSegment.UrlPart.Substring(index);

                        newRouteSegment.IncludeUrl(url, pipelineFactory);
                        return;
                    }
                }

                Children.Add(new StaticRouteSegment(url, pipelineFactory));
            }
        }

        private bool IsCharAtEquals(string source, string target, int index)
        {
            if (source.Length <= index || target.Length <= index)
                return false;

            return source[index] == target[index];
        }
    }
}
