using Neptuo.ComponentModel.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http.Converters
{
    /// <summary>
    /// Converter for converting string to <see cref="HttpMethod"/>.
    /// </summary>
    public class HttpMethodConverter : ConverterBase<string, HttpMethod>
    {
        public override bool TryConvert(string sourceValue, out HttpMethod targetValue)
        {
            targetValue = HttpMethod.KnownMethods[sourceValue];
            return targetValue != null;
        }
    }
}
