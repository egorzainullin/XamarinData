using System.Collections.Generic;
using System.Xml;

namespace XamarinData.Model
{
    public class DataParser
    {
        private readonly string _stringToParse;


        public DataParser(string stringToParse)
        {
            _stringToParse = stringToParse;
        }

        public IList<Offer> Parse()
        {
            var list = new List<Offer>();
            var doc = new XmlDocument();
            doc.LoadXml(_stringToParse);
            var root = doc.DocumentElement;
            const string path = @"yml_catalog/shop/offers/offer";
            foreach (XmlNode node in doc.SelectNodes(path))
            {
                var properties = new Dictionary<string, string>();
                var nodeID = node.Attributes["id"];
                var idValue = int.Parse(nodeID.Value);
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    var name = attribute.Name;
                    var value = attribute.Value;
                    if (!properties.ContainsKey(name))
                    {
                        properties.Add(name, value);
                    }
                }

                foreach (XmlNode childNode in node.ChildNodes)
                {
                    var name = childNode.Name;
                    var value = childNode.Value ?? string.Empty;
                    if (!properties.ContainsKey(name))
                    {
                        properties.Add(name, value);
                    }
                }

                var offer = new Offer(idValue, properties);
                list.Add(offer);
            }

            return list;
        }
    }
}