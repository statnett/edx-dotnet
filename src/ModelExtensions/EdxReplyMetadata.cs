using System;
using System.Xml.Serialization;

namespace Statnett.EdxLib.ModelExtensions
{
    [Serializable]
    [XmlType(TypeName = "edxReplyMetadata", Namespace = "")]
    public class EdxReplyMetadata
    {
        [XmlElement(ElementName = "receiveTimestamp", Namespace = "")]
        public DateValue ReceiveTimestamp { get; set; }

        [XmlElement(ElementName = "originalmessageID", Namespace = "")]
        public StringValue OriginalMessageId { get; set; }
    }
}