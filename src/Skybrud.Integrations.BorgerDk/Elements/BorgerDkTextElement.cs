using System.Collections.Generic;
using System.Xml.Linq;

namespace Skybrud.Integrations.BorgerDk.Elements {

    public class BorgerDkTextElement : BorgerDkElement {

        public string Title { get; internal set; }

        public string Content { get; internal set; }

        public IEnumerable<XElement> Children { get; internal set; }

        public override XElement ToXElement() {
            XElement xChildren = new XElement("xml");
            foreach (var child in Children) {
                xChildren.Add(child);
            }
            return new XElement(
                Type,
                new XElement("title", Title ?? ""),
                new XElement("html", new XCData(Content)),
                xChildren
            );
        }

    }

}