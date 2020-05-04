using System;
using System.Collections.Generic;
using System.Xml;
using XPathInjection.Models;

namespace XPathInjection.Utility
{
    public class OfferParser
    {
        public static List<Offer> QueryOffer(string title, string xmlPath)
        {

            // title = System.Xml.XmlConvert.EncodeName(title);

            string filter = "//offer[contains(title,'" + title + "')]";

            XmlDocument XmlDoc = new XmlDocument();

            XmlDoc.Load(xmlPath);

            XmlNode root = XmlDoc.DocumentElement;

            XmlNodeList nodeList = root.SelectNodes(filter);

            List<Offer> offers = new List<Offer>();

            foreach (XmlNode node in nodeList)
            {
                Offer offer = new Offer();
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.NodeType == XmlNodeType.Element)
                    {
                        #region offer
                        if (childNode.Name == "id")
                        {
                            offer.ID = childNode.InnerText;
                        }
                        else if (childNode.Name == "title")
                        {
                            offer.Title = childNode.InnerText;
                        }
                        else if (childNode.Name == "total")
                        {
                            offer.Total = Int32.Parse(childNode.InnerText);
                        }
                        else if (childNode.Name == "reference")
                        {
                            offer.Reference = childNode.InnerText;
                        }
                        #endregion
                    }
                }
                offers.Add(offer);
            }

            return offers;
        }
    }
}
