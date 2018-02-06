using System;
using System.Collections.Generic;

namespace Statnett.EdxLib.ModelExtensions
{
    public class MessageStatus
    {
        public Status Status { get; set; }
        public DateTime ChangeTimeStamp { get; set; }
        public string StatusText { get; set; }

        public override bool Equals(object obj)
        {
            var status = obj as MessageStatus;
            return status != null &&
                   Status == status.Status &&
                   ChangeTimeStamp == status.ChangeTimeStamp &&
                   StatusText == status.StatusText;
        }

        public override int GetHashCode()
        {
            var hashCode = 843061258;
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            hashCode = hashCode * -1521134295 + ChangeTimeStamp.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(StatusText);
            return hashCode;
        }
    }
}
