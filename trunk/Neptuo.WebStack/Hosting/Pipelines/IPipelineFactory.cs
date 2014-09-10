﻿using Neptuo.WebStack.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Hosting.Pipelines
{
    /// <summary>
    /// Factory for creating instances of pipelines.
    /// </summary>
    public interface IPipelineFactory : IActivator<IPipeline>
    { }
}
