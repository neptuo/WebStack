﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Routing
{
    /// <summary>
    /// Base implementation of <see cref="IRouteParameterCollection"/> using <see cref="Dictionary<,>"/>.
    /// </summary>
    public class RouteParameterCollection : IRouteParameterCollection
    {
        private Dictionary<string, IRouteParameter> storage = new Dictionary<string, IRouteParameter>();

        public IRouteParameterCollection Add(string parameterName, IRouteParameter parameter)
        {
            Ensure.NotNullOrEmpty(parameterName, "parameterName");
            Ensure.NotNull(parameter, "parameter");
            storage[parameterName] = parameter;
            return this;
        }

        public bool TryGet(string parameterName, out IRouteParameter parameter)
        {
            Ensure.NotNullOrEmpty(parameterName, "parameterName");
            return storage.TryGetValue(parameterName, out parameter);
        }
    }
}
