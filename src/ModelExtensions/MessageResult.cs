using System.Collections.Generic;

namespace Statnett.EdxLib.ModelExtensions
{
    public class MessageResult
    {
        public MessageStatus FinalMessageStatus { get; set; }
        public IEnumerable<MessageStatus> StatusHistory { get; set; }
        public EdxReplyMetadata EdxReplyMetadata { get; set; }
    }
}