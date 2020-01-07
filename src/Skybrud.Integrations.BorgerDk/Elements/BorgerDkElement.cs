using System.Xml.Linq;

namespace Skybrud.Integrations.BorgerDk.Elements {

    public abstract class BorgerDkElement {

        public string Type { get; internal set; }

        public abstract XElement ToXElement();

    }

}
