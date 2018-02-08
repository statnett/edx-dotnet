using System.Collections.Generic;
using System.Xml.Serialization;

namespace Statnett.EdxLib.ModelExtensions
{
    [System.Serializable]
    [XmlType(AnonymousType = true, Namespace = "edx.entsoe.eu.StatusDocument.xsd")]
    [XmlRoot(Namespace = "edx.entsoe.eu.StatusDocument.xsd")]
    public class StatusDocument
    {        
        [XmlElement(ElementName = "finalMessageStatus", Namespace = "")]
        public MessageStatus FinalMessageStatus { get; set; }

        [XmlElement(ElementName = "edxReplyMetadata", Namespace = "")]
        public EdxReplyMetadata EdxReplyMetadata { get; set; }

        [XmlElement(ElementName = "statusHistory", Namespace = "")]
        public MessageStatus[] StatusHistory { get; set; }

        public override bool Equals(object obj)
        {
            var result = obj as StatusDocument;
            return result != null &&
                   EqualityComparer<MessageStatus>.Default.Equals(FinalMessageStatus, result.FinalMessageStatus) &&
                   EqualityComparer<IEnumerable<MessageStatus>>.Default.Equals(StatusHistory, result.StatusHistory) &&
                   EqualityComparer<EdxReplyMetadata>.Default.Equals(EdxReplyMetadata, result.EdxReplyMetadata);
        }

        public override int GetHashCode()
        {
            var hashCode = -265851890;
            hashCode = hashCode * -1521134295 + EqualityComparer<MessageStatus>.Default.GetHashCode(FinalMessageStatus);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<MessageStatus>>.Default.GetHashCode(StatusHistory);
            hashCode = hashCode * -1521134295 + EqualityComparer<EdxReplyMetadata>.Default.GetHashCode(EdxReplyMetadata);
            return hashCode;
        }
    }
}