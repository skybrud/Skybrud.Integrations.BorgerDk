using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;
using HtmlAgilityPack;

namespace Skybrud.Integrations.BorgerDk {

    public static class BorgerDkHelpers {

        internal static HtmlNode[] GetNonTextChildren(HtmlNode node) {
            return node.ChildNodes.Where(child => !(child is HtmlTextNode)).ToArray();
        }

        public static XElement ToXElement(HtmlNode node) {

            XElement xElement;

            try {
                xElement = new XElement(node.Name);
            } catch (Exception ex) {
                throw new Exception("Unable to create new XML element with name: " + node.Name, ex);
            }
            
            HashSet<string> attributes = new HashSet<string>();

            foreach (HtmlAttribute attr in node.Attributes) {

                // Prevent duplicate attributes 
                if (attributes.Contains(attr.Name)) continue;
                attributes.Add(attr.Name);

                xElement.Add(new XAttribute(attr.Name, attr.Value.Trim()));

            }

            foreach (var child in node.ChildNodes) {
                if (child is HtmlTextNode) {
                    string innerText = child.InnerText;
                    innerText = innerText.Replace("&amp;nbsp;", " ");
                    innerText = innerText.Replace("&nbsp;", " ");
                    innerText = innerText.Replace("&#160;", " ");
                    innerText = innerText.Replace("&amp;#160;", " ");
                    //innerText = innerText.Trim(); // Trimming gives some unexpected results with inline elements
                    if (innerText.Trim() == "") continue;
                    xElement.Add(HttpUtility.HtmlDecode(innerText));
                } else {

                    if (child.Name == "#comment") continue;

                    if (child.Name == "a") {
                        if (child.InnerText == "") continue;
                        if (child.GetAttributeValue("target", "") == "") {
                            child.SetAttributeValue("target", "_blank");
                            if (!child.InnerHtml.Contains("nyt vindue")) {
                                child.InnerHtml += " (nyt vindue)";
                            }
                        }
                    }

                    if (child.Name == "br") {
                        xElement.Add(new XElement("br"));
                        continue;
                    }

                    if (child.Name == "ul" || child.Name == "ol" || child.Name == "li") {
                        xElement.Add(ToXElement(child));
                        continue;
                    }

                    try {
                        XElement xChild = ToXElement(child);
                        if (!xChild.Attributes().Any() && xChild.Name != "br" && xChild.Name != "hr" && xChild.Value == "") continue;
                        xElement.Add(xChild);
                    } catch (Exception ex) {
                        throw new Exception("Failed converting HTML node to XML\r\n\r\nName: " + child.Name + "\r\n\r\n" + child.ParentNode.OuterHtml, ex);
                    }
                }
            }

            xElement.Add("");

            return xElement;

        }

        internal static XElement CleanLists(XElement root) {
            IEnumerable<XElement> listItems = root.XPathSelectElements("li");
            foreach (var item in listItems) {
                if (item.Elements().Count() == 1) {
                    var first = item.Elements().First();
                    if (first.Name == "div" || first.Name == "span") {
                        var anchor = first.XPathSelectElement("descendant-or-self::a");
                        if (anchor != null) {
                            first.Remove();
                            item.Add(anchor);
                        }
                    }
                }
            }
            return root;
        }

        public static XElement CleanMicroArticle(XElement root) {

            foreach (var span in root.XPathSelectElements("descendant::span").ToArray()) {
                if (IsEmptyOrWhitespace(span)) {
                    try {
                        span.Remove();
                    } catch (Exception) {
                        // ignore exception
                    }
                }
            }

            foreach (var paragraph in root.XPathSelectElements("descendant::p").ToArray()) {
                if (IsEmptyOrWhitespace(paragraph)) {
                    try {
                        paragraph.Remove();
                    } catch (Exception) {
                        // ignore exception
                    }
                }
            }

            foreach (var div in root.XPathSelectElements("descendant::div").ToArray()) {
                if (IsEmptyOrWhitespace(div)) {
                    try {
                        div.Remove();
                    } catch (Exception) {
                        // ignore exception
                    }
                }
            }

            foreach (var li in root.XPathSelectElements("descendant::li").ToArray()) {
                if (li.Elements().Count() == 1) {
                    var first = li.Elements().First();
                    if (first.Name == "div" || first.Name == "p") {
                        foreach (var node in first.Nodes()) {
                            li.Add(node);
                        }
                        first.Remove();
                    }
                }
            }
            
            return root;
        
        }

        internal static bool IsEmptyOrWhitespace(XElement e) {
            return !e.Elements().Any() && HttpUtility.HtmlDecode(e.Value).Trim() == "";
        }

    }

}
