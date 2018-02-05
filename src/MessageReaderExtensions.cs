using System;
using System.Text;
using System.Xml;
using Amqp;
using Statnett.EdxLib.ModelExtensions;

namespace Statnett.EdxLib
{
    public static class MessageReaderExtensions
    {
        public static MessageStatus DecodeBodyAsMessageStatus(this Message msg)
        {
            var xml = msg.DecodeBodyAsXml();

            if (xml.Name != "StatusDocument")
            {
                throw new ArgumentException(string.Format("Malformed body: {0}", xml), "msg");
            }

            return new MessageStatus();
        }

        public static XmlDocument DecodeBodyAsXml(this Message message)
        {
            if (message == null || message.Body == null)
            {
                throw new ArgumentException("message");
            }

            var doc = new XmlDocument();
            var text = message.DecodeBodyAsString(string.Empty);
            doc.LoadXml(text);
            return doc;
        }

        public static string DecodeBodyAsString(this Message message, string defaultValue = null)
        {
            var body = message.Body as byte[];
            if (body != null)
            {
                var content = new char[body.Length];
                int charsUsed;
                int bytesUsed;
                bool completed;

                Encoding.UTF8.GetDecoder().Convert(body, 0, body.Length, content, 0, body.Length, true, out bytesUsed, out charsUsed, out completed);
                if (completed)
                {
                    return new string(content);
                }

                if (defaultValue != null)
                {
                    return defaultValue;
                }
            }
            else
            {
                var bodyType = message.Body != null ? message.Body.GetType().Name : "<null>";
                var result = string.Format("Expected byte array, got {0}", bodyType);
                throw new Exception(result); // TODO: Custom exception
            }

            throw new Exception("Conversion error: Wrong encoding"); // TODO: Custom exception
        }

        public static string GetReceiver(this Message message)
        {
            return GetProperty(message, Constants.ApplicationPropertyKeys.Receiver);
        }

        public static string GetReceiverCode(this Message message)
        {
            return GetProperty(message, Constants.ApplicationPropertyKeys.ReceiverCode);
        }

        public static string GetBusinessType(this Message message)
        {
            return GetProperty(message, Constants.ApplicationPropertyKeys.BusinessType);
        }

        public static string GetBusinessMessageId(this Message message)
        {
            return GetProperty(message, Constants.ApplicationPropertyKeys.BusinessMessageId);
        }

        public static string GetSenderApplication(this Message message)
        {
            return GetProperty(message, Constants.ApplicationPropertyKeys.SenderApplication);
        }

        public static string GetProperty(Message message, string propertyName)
        {
            if (!message.ApplicationProperties.Map.ContainsKey(propertyName))
            {
                return null;
            }
            return message.ApplicationProperties[propertyName] as string;
        }
    }
}
