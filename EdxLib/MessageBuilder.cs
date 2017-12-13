using System;
using System.Text;
using Amqp;
using Amqp.Framing;

#pragma warning disable 1573

namespace EdxLib
{
    public static class MessageBuilder
    {
        /// <summary>
        /// Create a text message with content ecoded as UTF-8
        /// </summary>
        /// <param name="content">Content to send</param>
        /// <returns></returns>
        public static Message CreateMessage(string businessType, string content)
        {
            return CreateMessage(businessType, Encoding.UTF8.GetBytes(content));
        } 

        /// <summary>
        /// Create a new message with a given body and content settings
        /// </summary>
        /// <param name="body"></param>
        /// <param name="businessType">Required for EDX reception</param>
        /// <returns></returns>
        public static Message CreateMessage(string businessType, byte[] body)
        {            
            var msg = new Message(body)
            {
                BodySection = new Data { Binary = body },                
                Properties = new Properties
                {
                    MessageId = Guid.NewGuid().ToString(),
                    ContentType = "application/octetstream",
                },
                ApplicationProperties = new ApplicationProperties
                {
                    Map = { { Constants.ApplicationPropertyKeys.BusinessType, businessType } }
                }
            };

            return msg;
        }

        /// <summary>
        /// Used to track conversations / sagas
        /// </summary>
        /// <param name="correlationId">Original message</param>
        /// <returns></returns>
        public static Message WithCorrelationId(this Message message, Guid correlationId)
        {
            return message.WithCorrelationId(correlationId.ToString());
        }

        /// <summary>
        /// Used to track conversations / sagas
        /// </summary>
        /// <param name="correlationId">Original message</param>
        /// <returns></returns>
        public static Message WithCorrelationId(this Message message, string correlationId)
        {
            message.EnsurePropertiesExist();
            message.Properties.CorrelationId = correlationId;
            return message;
        }

        /// <summary>
        /// Point the message to a given receiver address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static Message WithReceiverAddress(this Message message, string address)
        {
            message.EnsureApplicationPropertiesExist();
            message.ApplicationProperties.Map.Add(Constants.ApplicationPropertyKeys.Receiver, address);
            message.ApplicationProperties.Map.Add(Constants.ApplicationPropertyKeys.ReceiverCode, address);
            return message;
        }

        /// <summary>
        /// Set address based on endpoint and service
        /// </summary>
        /// <param name="endpoint">ECP endpoint address, usually a GLN number</param>
        /// <param name="service">Service you want to reach on the endpoint, e.g. SERVICE-FOS</param>
        /// <returns>self with receiver and receiverCode set to endpoint@address</returns>
        public static Message WithEdxToolboxSpecificServiceAddress(this Message message, string endpoint, string service)
        {
            return message.WithReceiverAddress(string.Format("{0}@{1}", endpoint, service));
        }

        public static Message WithBusinessMessageId(this Message message, Guid businessMessageId)
        {
            return message.WithBusinessMessageId(businessMessageId.ToString());
        }

        public static Message WithBusinessMessageId(this Message message, string businessMessageId)
        {
            message.EnsureApplicationPropertiesExist();
            message.ApplicationProperties.Map.Add(Constants.ApplicationPropertyKeys.BusinessMessageId, businessMessageId);
            return message;
        }

        public static Message WithSenderApplication(this Message message, string senderApplication)
        {
            message.EnsureApplicationPropertiesExist();
            message.ApplicationProperties.Map.Add(Constants.ApplicationPropertyKeys.SenderApplication, senderApplication);
            return message;
        }

        /// <summary>
        /// Guard in case property setter  methods are used on Message objects created manually (not using EdxMessageFactory)
        /// </summary>
        /// <param name="message"></param>
        private static void EnsurePropertiesExist(this Message message)
        {
            if (message.Properties == null)
            {
                message.Properties = new Properties();
            }
        }

        /// <summary>
        /// Guard in case property setter  methods are used on Message objects created manually (not using EdxMessageFactory)
        /// </summary>
        /// <param name="message"></param>
        private static void EnsureApplicationPropertiesExist(this Message message)
        {
            if (message.ApplicationProperties == null)
            {
                message.ApplicationProperties = new ApplicationProperties();
            }
        }
    }
}
