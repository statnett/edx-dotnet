using System;
using Amqp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Statnett.EdxLib.Tests
{
    [TestClass]
    public class MessageReaderTests
    {
        private Message _message;

        [TestInitialize]
        public void Setup()
        {
            _message = FluentMessageBuilder.CreateMessage("BusinessType", "Hello World!");
        }

        [TestMethod]
        public void CanReadMessageType()
        {
            var result = _message.GetBusinessType();
            Assert.AreEqual("BusinessType", result);
        }

        [TestMethod]
        public void CanDecodeBodyAsString()
        {
            var result = _message.DecodeBodyAsString();
            Assert.AreEqual("Hello World!", result);
        }

        [TestMethod]
        public void CanGetReceiver()
        {
            _message.WithReceiverAddress("MyReceiver");

            var result = _message.GetReceiver();

            Assert.AreEqual("MyReceiver", result);
        }

        [TestMethod]
        public void GivenReceiver_CanGetReceiverCode()
        {
            _message.WithReceiverAddress("MyReceiver");

            var result = _message.GetReceiverCode();

            Assert.AreEqual("MyReceiver", result);
        }

        [TestMethod]
        public void Given_correlationid_from_guid_CorrelationId_is_returned_as_string()
        {
            var correlationId = Guid.NewGuid();
            _message.WithCorrelationId(correlationId);

            var result = _message.Properties.CorrelationId;

            Assert.AreEqual(correlationId.ToString(), result);
        }
    }
}
