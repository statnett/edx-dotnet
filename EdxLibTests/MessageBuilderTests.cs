using System;
using System.Text;
using Amqp;
using EdxLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EdxLibTests
{
    [TestClass]
    public class MessageBuilderTests
    {
        private Message _message;

        [TestInitialize]
        public void Setup()
        {
            _message = MessageBuilder.CreateMessage("MyBusinessType", "Message body");
        }

        [TestMethod]
        public void CreateMessage_SetsMessageid()
        {
            var result = _message.Properties.MessageId;
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void CreateMessage_SetsBusinessType()
        {
            var result = _message.ApplicationProperties["businessType"];
            Assert.AreEqual("MyBusinessType", result);
        }

        [TestMethod]
        public void CreateMessage_SetsContent()
        {
            var result = Encoding.UTF8.GetString((byte[]) _message.Body);
            Assert.AreEqual("Message body", result);
        }

        [TestMethod]
        public void WithCorrelationId_as_guid()
        {
            var correlationId = Guid.NewGuid();
            _message.WithCorrelationId(correlationId);
            
            var result = _message.Properties.CorrelationId;
            Assert.AreEqual(correlationId.ToString(), result);
        }

        [TestMethod]
        public void WithCorrelationId_as_string()
        {
            var correlationId = Guid.NewGuid().ToString();
            _message.WithCorrelationId(correlationId);

            var result = _message.Properties.CorrelationId;
            Assert.AreEqual(correlationId, result);
        }

        [TestMethod]
        public void WithAddress_sets_receiver()
        {
            _message.WithReceiverAddress("SENDTO");
            var result = _message.ApplicationProperties["receiver"];
            Assert.AreEqual("SENDTO", result);
        }

        [TestMethod]
        public void WithAddress_sets_receiverCode()
        {
            _message.WithReceiverAddress("SERVICE");
            var result = _message.ApplicationProperties["receiverCode"];
            Assert.AreEqual("SERVICE", result);
        }

        [TestMethod]
        public void WithEdxToolboxSpecificServiceAddress_sets_receiver()
        {
            _message.WithEdxToolboxSpecificServiceAddress("ENDPOINT", "SERVICE");
            var result = _message.ApplicationProperties["receiver"];
            Assert.AreEqual("ENDPOINT@SERVICE", result);
        }

        [TestMethod]
        public void WithEdxToolboxSpecificServiceAddress_sets_receiverCode()
        {
            _message.WithEdxToolboxSpecificServiceAddress("ENDPOINT", "SERVICE");
            var result = _message.ApplicationProperties["receiverCode"];
            Assert.AreEqual("ENDPOINT@SERVICE", result);
        }

    }
}
