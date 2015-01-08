using Neptuo.ComponentModel.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http.Converters
{
    /// <summary>
    /// Converter for converting int status code to <see cref="HttpStatus"/>.
    /// </summary>
    public class HttpStatusConverter : ConverterBase<int, HttpStatus>
    {
        public override bool TryConvert(int sourceValue, out HttpStatus targetValue)
        {
            targetValue = HttpStatus.KnownStatuses[sourceValue];
            if (targetValue == null)
            {
                if (sourceValue > 0)
                {
                    targetValue = new HttpStatus(sourceValue);
                    return true;
                }

            }

            return false;
        }
    }
}
