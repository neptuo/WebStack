﻿using Neptuo.ComponentModel.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http.Converters
{
    /// <summary>
    /// Converter for converting string value to <see cref="HttpMediaType"/>.
    /// </summary>
    public class HttpMediaTypeConverter : IConverter<string, HttpMediaType>, IConverter<string, IEnumerable<HttpMediaType>>, IConverter<HttpMediaType, string>
    {
        private readonly List<HttpMediaType> knownMediaTypes = new List<HttpMediaType>
        {
            HttpMediaType.Html,
            HttpMediaType.Xhtml,
            HttpMediaType.Xml,
            HttpMediaType.Json
        };

        #region Single value

        protected bool TryConvertValue(string sourceValue, out HttpMediaType targetValue)
        {
            if (String.IsNullOrEmpty(sourceValue))
            {
                targetValue = HttpMediaType.Unknown;
                return true;
            }

            foreach (HttpMediaType knownMediaType in knownMediaTypes)
            {
                if (knownMediaType.TextValue == sourceValue || knownMediaType.SupportedTextValues.Contains(sourceValue))
                {
                    targetValue = knownMediaType;
                    return true;
                }
            }

            targetValue = new HttpMediaType(sourceValue);
            return true;
        }

        bool IConverter<string, HttpMediaType>.TryConvert(string sourceValue, out HttpMediaType targetValue)
        {
            return TryConvertValue(sourceValue, out targetValue);
        }

        bool IConverter.TryConvertGeneral(Type sourceType, Type targetType, object sourceValue, out object targetValue)
        {
            if (sourceType != typeof(string))
            {
                targetValue = null;
                return false;
            }

            if (targetType == typeof(HttpMediaType))
            {
                HttpMediaType value;
                if (TryConvertValue((string)sourceValue, out value))
                {
                    targetValue = value;
                    return true;
                }
            }
            else if (targetType == typeof(IEnumerable<HttpMediaType>))
            {
                IEnumerable<HttpMediaType> value;
                if (TryConvertValue((string)sourceValue, out value))
                {
                    targetValue = value;
                    return true;
                }
            }

            targetValue = null;
            return false;
        }

        #endregion

        #region Collection value

        protected bool TryConvertValue(string sourceValue, out IEnumerable<HttpMediaType> targetValue)
        {
            if (String.IsNullOrEmpty(sourceValue))
            {
                targetValue = null;
                return false;
            }

            string[] values = sourceValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<HttpMediaType> result = new List<HttpMediaType>();
            foreach (string value in values)
            {
                HttpMediaType itemValue;
                if (TryConvertValue(value, out itemValue))
                    result.Add(itemValue);
            }

            targetValue = result;
            return true;
        }

        bool IConverter<string, IEnumerable<HttpMediaType>>.TryConvert(string sourceValue, out IEnumerable<HttpMediaType> targetValue)
        {
            return TryConvertValue(sourceValue, out targetValue);
        }

        #endregion

        public bool TryConvert(HttpMediaType sourceValue, out string targetValue)
        {
            if (sourceValue == null)
                targetValue = null;

            targetValue = sourceValue.TextValue;
            return true;
        }
    }
}
