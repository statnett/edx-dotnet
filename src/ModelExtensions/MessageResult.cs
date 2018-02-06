using System.Collections.Generic;

namespace Statnett.EdxLib.ModelExtensions
{
    public class MessageResult
    {
        public MessageStatus FinalMessageStatus { get; set; }
//        public IEnumerable<MessageStatus> StatusHistory { get; set; }
        public EdxReplyMetadata EdxReplyMetadata { get; set; }

        public override bool Equals(object obj)
        {
            var result = obj as MessageResult;
            return result != null &&
                   EqualityComparer<MessageStatus>.Default.Equals(FinalMessageStatus, result.FinalMessageStatus) &&
//                   EqualityComparer<IEnumerable<MessageStatus>>.Default.Equals(StatusHistory, result.StatusHistory) &&
                   EqualityComparer<EdxReplyMetadata>.Default.Equals(EdxReplyMetadata, result.EdxReplyMetadata);
        }

        public override int GetHashCode()
        {
            var hashCode = -265851890;
            hashCode = hashCode * -1521134295 + EqualityComparer<MessageStatus>.Default.GetHashCode(FinalMessageStatus);
//            hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<MessageStatus>>.Default.GetHashCode(StatusHistory);
            hashCode = hashCode * -1521134295 + EqualityComparer<EdxReplyMetadata>.Default.GetHashCode(EdxReplyMetadata);
            return hashCode;
        }
    }
}