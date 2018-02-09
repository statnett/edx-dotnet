using System;
using System.Xml.Serialization;

namespace Statnett.EdxLib.ModelExtensions
{
    [Serializable]
    public class StringValue
    {
        [XmlAttribute(AttributeName = "v", Namespace = "")]
        public string Value { get; set; }
    }
}