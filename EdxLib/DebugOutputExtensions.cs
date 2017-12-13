using System.Text;
using Amqp;
using Amqp.Types;

namespace EdxLib
{
    public static class DebugOutputExtensions
    {
        public static string ToVerboseString(this Message message, bool printBody = false)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Message content:");

            sb.AppendLine("- Properties:");            
            sb.AppendLineIfValue(message.Properties.ContentEncoding, "  > ContentEncoding: \t{0}");
            sb.AppendLineIfValue(message.Properties.ContentType, "  > ContentType:\t{0}");
            sb.AppendLineIfValue(message.Properties.CorrelationId, "  > CorrelationId:\t{0}");
            sb.AppendLineIfValue(message.Properties.GroupId, "  > GroupId:\t\t{0}");
            sb.AppendLineIfValue(message.Properties.MessageId, "  > MessageId:\t\t{0}");
            sb.AppendLineIfValue(message.Properties.ReplyTo, "  > ReplyTo:\t\t{0}");
            sb.AppendLineIfValue(message.Properties.ReplyToGroupId, "  > ReplyToGroupId:\t{0}");
            sb.AppendLineIfValue(message.Properties.Subject, "  > Subject:\t\t{0}");
            sb.AppendLineIfValue(message.Properties.To, "  > To:\t\t\t{0}");
            sb.AppendLineIfValue(message.Properties.UserId, "  > UserId:\t\t {0}");

            sb.AppendLine("- ApplicationProperties:");
            sb.AppendMap(message.ApplicationProperties);

            if (message.MessageAnnotations != null)
            {
                sb.AppendLine("- MessageAnnotations:");
                sb.AppendMap(message.MessageAnnotations);
            }
            if (message.DeliveryAnnotations != null)
            {
                sb.AppendLine("- DeliveryAnnotations:");
                foreach (var key in message.DeliveryAnnotations.Map.Keys)
                {
                    var value = message.DeliveryAnnotations[key].ToString();
                    sb.AppendLine(key.ToString().Length >= 10
                        ? string.Format("  > {0}:\t{1}", key, value)
                        : string.Format("  > {0}:\t\t{1}", key, value));
                }
            }

            if (printBody && message.Body != null)
            {
                sb.AppendLine("- Body:");
                sb.AppendLine(message.DecodeBodyAsString());
            }
            return sb.ToString();
        }

        private static void AppendMap(this StringBuilder sb, DescribedMap dictionary)
        {
            foreach (var key in dictionary.Map.Keys)
            {
                var value = dictionary[key].ToString();
                var prop = key.ToString().Length >= 10
                    ? string.Format("  > {0}:\t", key) + "{0}"
                    : string.Format("  > {0}:\t\t", key) + "{0}";
                sb.AppendLineIfValue(value, string.Format("{0}{{0}}", prop));
            }
        }

        private static void AppendLineIfValue(this StringBuilder sb, object value, string format)
        {
            if (value != null)
            {
                sb.AppendLine(string.Format(format, value.ToString()));
            }
        }
    }
}
