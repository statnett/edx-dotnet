using System;
using System.Xml.Serialization;

namespace Statnett.EdxLib.ModelExtensions
{
    [Serializable]
    public class DateValue
    {
        [XmlAttribute(AttributeName = "v", Namespace = "")]
        public DateTime Value { get; set; }
    }
}