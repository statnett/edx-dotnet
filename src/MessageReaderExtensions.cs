using System;
using System.Text;
using System.Xml;
using Amqp;
using Statnett.EdxLib.Exceptions;
using Statnett.EdxLib.ModelExtensions;

namespace Statnett.EdxLib
{
    public static class MessageReaderExtensions
    {
        public static MessageResult DecodeBodyAsMessageStatus(this Message msg)
        {
            var xml = msg.DecodeBodyAsXml();

            if (xml.DocumentElement == null || !"edx:StatusDocument".Equals(xml.DocumentElement.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException(string.Format("Malformed body: {0}", xml), "msg");
            }

            var result = new MessageResult();
            
            ExtractFinalMessageStatus(xml, result);

            return result;
        }

        private static void ExtractFinalMessageStatus(XmlDocument xml, MessageResult result)
        {
            var finalXml = xml.DocumentElement["finalMessageStatus"];
            if (finalXml != null)
            {
                var final = new MessageStatus();

                var status = finalXml["status"].Attributes["v"].InnerText;
                final.Status = (Status) Enum.Parse(typeof(Status), status, true);

                var statusText = finalXml["statusText"];
                if (statusText != null)
                {
                    final.StatusText = statusText.Attributes["v"].InnerText;
                }

                var timeStamp = finalXml["changeTimestamp"].Attributes["v"].InnerText;
                final.ChangeTimeStamp = DateTime.Parse(timeStamp).ToUniversalTime();

                result.FinalMessageStatus = final;
            }
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
                throw new EdxException(result);
            }

            throw new EdxException("Conversion error: Wrong encoding");
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
