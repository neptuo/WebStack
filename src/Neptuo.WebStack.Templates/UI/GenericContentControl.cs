using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.UI
{
    public class GenericContentControl : IContentControl, IHtmlAttributeCollectionAware
    {
        private IComponentManager componentManager;

        public string TagName { get; set; }
        public ICollection<object> Content { get; set; }
        public HtmlAttributeCollection HtmlAttributes { get; set; }

        public GenericContentControl()
        {
            Content = new List<object>();
            HtmlAttributes = new HtmlAttributeCollection();
        }

        public void OnInit(IComponentManager componentManager)
        {
            this.componentManager = componentManager;
            foreach (object item in Content)
                componentManager.OnInit(item);
        }

        public void Render(IHtmlWriter writer)
        {
            if (!String.IsNullOrEmpty(TagName))
            {
                writer.Tag(TagName);

                foreach (KeyValuePair<string, string> attribute in HtmlAttributes)
                    writer.Attribute(attribute.Key, attribute.Value);
            }

            foreach (object item in Content)
                componentManager.Render(item, writer);

            if (!String.IsNullOrEmpty(TagName))
                writer.CloseTag();
        }
    }
}
