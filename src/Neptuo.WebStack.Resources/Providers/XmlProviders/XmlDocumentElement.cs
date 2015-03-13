﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Neptuo.WebStack.Resources.Providers.XmlProviders
{
    internal class XmlDocumentElement : IXmlElement
    {
        private XmlElement element;

        public XmlDocumentElement(XmlElement element)
        {
            Ensure.NotNull(element, "element");
            this.element = element;
        }

        public string Name
        {
            get { return element.Name; }
        }

        public string GetAttributeValue(string attributeName)
        {
            Ensure.NotNullOrEmpty(attributeName, "attributeName");
            return element.GetAttribute(attributeName);
        }

        public IEnumerable<IXmlElement> EnumerateChildElements(string elementName)
        {
            Ensure.NotNullOrEmpty(elementName, "elementName");
            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element && node.Name == elementName)
                    yield return new XmlDocumentElement((XmlElement)node);
            }
        }

        public IEnumerable<string> EnumerateAttributeNames()
        {
            foreach (XmlAttribute attribute in element.Attributes)
                yield return attribute.Name;
        }
    }
}
