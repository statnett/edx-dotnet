using System;
using System.Xml.Serialization;

namespace Statnett.EdxLib.ModelExtensions
{
    [Serializable]
    public class MessageStatus
    {
        [XmlElement(ElementName = "status", Namespace = "")]
        public StatusValue Status { get; set; }

        [XmlElement(ElementName = "changeTimestamp", Namespace = "")]
        public DateValue ChangeTimeStamp { get; set; }

        [XmlElement(ElementName = "statusText", Namespace = "")]
        public StringValue StatusText { get; set; }
    }
}
