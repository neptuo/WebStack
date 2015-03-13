using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Describes media type of request and response.
    /// </summary>
    public class HttpMediaType : IEquatable<HttpMediaType>
    {
        /// <summary>
        /// Text value of content type.
        /// </summary>
        /// <example>
        /// application/json
        /// </example>
        public string TextValue { get; private set; }

        /// <summary>
        /// Enumeration of supported text values.
        /// </summary>
        public ICollection<string> SupportedTextValues { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="textValue"/> as main media type 
        /// and optional enumeration of other matching media types.
        /// </summary>
        /// <param name="textValue">Text value of content type.</param>
        /// <param name="supportedTextValues">Enumeration of optional supported text values.</param>
        public HttpMediaType(string textValue, params string[] supportedTextValues)
        {
            Ensure.NotNull(textValue, "textValue");
            TextValue = textValue;

            if (supportedTextValues != null)
                SupportedTextValues = new HashSet<string>(supportedTextValues);
            else
                SupportedTextValues = new HashSet<string>();

            SupportedTextValues.Add(TextValue);
        }

        protected HttpMediaType()
        {
            SupportedTextValues = new List<string>();
        }

        #region Known types

        /// <summary>
        /// Xhtml media type.
        /// </summary>
        public static HttpMediaType Xhtml = new HttpMediaType("application/xhtml+xml", "application/xhtml");

        /// <summary>
        /// Html media type.
        /// </summary>
        public static HttpMediaType Html = new HttpMediaType("text/html");

        /// <summary>
        /// Xml media type.
        /// </summary>
        public static HttpMediaType Xml = new HttpMediaType("application/xml", "text/xml");

        /// <summary>
        /// Json media type.
        /// </summary>
        public static HttpMediaType Json = new HttpMediaType("application/json", "text/json");

        /// <summary>
        /// 'Any' media type.
        /// </summary>
        public static HttpMediaType Any = new HttpAnyMediaType();

        /// <summary>
        /// Unknown media type.
        /// TODO: Fix 'unknown' value.
        /// </summary>
        public static HttpMediaType Unknown = new HttpUnknownMediaType();

        #endregion

        #region Equality comparision

        public override bool Equals(object obj)
        {
            return Equals(obj as HttpMediaType);
        }

        /// <summary>
        /// Equality comparition based on matching at least one supported text value.
        /// </summary>
        /// <param name="other">Other media type definition.</param>
        /// <returns><c>true</c> if this and <paramref name="other"/> has shared at least one supported text value.</returns>
        public virtual bool Equals(HttpMediaType other)
        {
            if (other == null)
                return false;

            foreach (string textValue in SupportedTextValues)
            {
                foreach (string otherValue in other.SupportedTextValues)
                {
                    if (textValue == otherValue)
                        return true;
                }
            }

            return false;
        }

        public static bool operator ==(HttpMediaType first, HttpMediaType second)
        {
            if (Object.Equals(first, null))
            {
                if (Object.Equals(second, null))
                    return true;
                else
                    return false;
            }

            return first.Equals(second);
        }

        public static bool operator !=(HttpMediaType first, HttpMediaType second)
        {
            if (Object.Equals(first, null))
            {
                if (Object.Equals(second, null))
                    return false;
                else
                    return true;
            }

            return !first.Equals(second);
        }

        public override int GetHashCode()
        {
            return TextValue.GetHashCode() ^ SupportedTextValues.GetHashCode();
        }

        #endregion
    }

    public class HttpUnknownMediaType : HttpMediaType
    {
        public override bool Equals(HttpMediaType other)
        {
            HttpUnknownMediaType unknown = other as HttpUnknownMediaType;
            return unknown != null;
        }
    }

    public class HttpAnyMediaType : HttpMediaType
    {
        public HttpAnyMediaType()
            : base("*/*")
        { }

        public override bool Equals(HttpMediaType other)
        {
            return true;
        }
    }
}
