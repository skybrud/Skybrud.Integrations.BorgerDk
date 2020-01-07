using System.Collections.Generic;
using System.Xml.Linq;

namespace Skybrud.Integrations.BorgerDk.Elements {
    
    public class BorgerDkMicroArticle {

        public BorgerDkBlockElement Parent { get; internal set; }

        public string Id { get; internal set; }

        public string Title { get; internal set; }

        public string TitleType { get; internal set; }

        public string Content { get; internal set; }

        public IEnumerable<XElement> Children { get; internal set; }

        public XElement ToXElement() {
            
            XElement xChildren = new XElement("xml");
            foreach (var child in Children) {
                if (child.Name == "a" && child.Value == "") continue;
                xChildren.Add(child);
            }

            return new XElement(
                "microArticle",
                new XAttribute("id", Id),
                new XElement("title", new XCData(Title)),
                new XElement("titletype", TitleType),
                new XElement("html", new XCData(Content)),
                xChildren
            );
        
        }

    }

}