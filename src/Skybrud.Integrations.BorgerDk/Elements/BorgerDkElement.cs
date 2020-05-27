using System.Xml.Linq;

namespace Skybrud.Integrations.BorgerDk.Elements {

    public abstract class BorgerDkElement {

        public string Id { get; internal set; }

        public string Type { get; internal set; }

    }

}