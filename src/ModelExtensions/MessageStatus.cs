using System;

namespace Statnett.EdxLib.ModelExtensions
{
    public class MessageStatus
    {
        public Status Status { get; set; }
        public DateTime ChangeTimeStamp { get; set; }
        public string StatusText { get; set; }
    }
}
