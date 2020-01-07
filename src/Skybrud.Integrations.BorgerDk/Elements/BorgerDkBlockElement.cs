using System.Xml.Linq;

namespace Skybrud.Integrations.BorgerDk.Elements {
    
    public class BorgerDkBlockElement : BorgerDkElement {

        public BorgerDkMicroArticle[] MicroArticles { get; internal set; }

        public override XElement ToXElement() {
            
            XElement xKernetekst = new XElement(
                Type
                );

            foreach (var micro in MicroArticles) {
                xKernetekst.Add(micro.ToXElement());
            }

            return xKernetekst;
        
        }

    }

}