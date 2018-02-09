using System;
using System.Xml.Serialization;

namespace Statnett.EdxLib.ModelExtensions
{
    [Serializable]
    public class StatusValue
    {
        [XmlAttribute(AttributeName = "v", Namespace = "")]
        public Status Value { get; set; }
    }
}