using System;
using Amqp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statnett.EdxLib.ModelExtensions;

namespace Statnett.EdxLib.Tests
{
    [TestClass]
    public class ReplyMessageReceiverSuccessMessageTests
    {
        private StatusDocument _statusDocument;

        [TestInitialize]
        public void Setup()
        {
            var message = CreateSuccessfulMessage();
            _statusDocument = message.DecodeBodyAsMessageStatus();
        }

        [TestMethod]
        public void ReadsEdxMetadata()
        {
            var data = _statusDocument.EdxReplyMetadata;
            Assert.AreEqual(new DateTime(2017, 11, 13, 15, 52, 40, DateTimeKind.Utc), data.ReceiveTimestamp.Value);
            Assert.AreEqual("fc1546ff-6ea7-40e5-89a1-839e8a5aa18f", data.OriginalMessageId.Value);
        }

        [TestMethod]
        public void GivenNoSuccessfullySentStatus_ShowsIsSuccessfulltSentFalse()
        {
            var status = _statusDocument.IsSuccessfullySent();
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void ReadsStatusHistory()
        {
            Assert.AreEqual(Status.Accepted, _statusDocument.StatusHistory[0].Status.Value);
            Assert.AreEqual(Status.Sent, _statusDocument.StatusHistory[1].Status.Value);
            Assert.AreEqual(Status.SuccessfullySent, _statusDocument.StatusHistory[2].Status.Value);
            Assert.AreEqual(Status.Delivered, _statusDocument.StatusHistory[3].Status.Value);
            Assert.AreEqual(Status.Received, _statusDocument.StatusHistory[4].Status.Value);
        }


        private static Message CreateSuccessfulMessage()
        {
            /*
<edx:StatusDocument xmlns:edx="edx.entsoe.eu.StatusDocument.xsd" xmlns:common="edx.entsoe.eu.common.xsd">
    <finalMessageStatus>
        <status v="RECEIVED"/>
        <changeTimestamp v="2017-11-13T15:52:42Z"/>
    </finalMessageStatus>

    <edxReplyMetadata>
        <originalmessageID v="fc1546ff-6ea7-40e5-89a1-839e8a5aa18f"/>
        <receiveTimestamp v="2017-11-13T15:52:40Z"/>
    </edxReplyMetadata>

    <statusHistory>
        <messageStatus>
            <status v="ACCEPTED"/>
            <changeTimestamp v="2017-11-13T15:52:40Z"/>
        </messageStatus>
        <messageStatus>
            <status v="SENT"/>
            <changeTimestamp v="2017-11-13T15:52:41Z"/>
        </messageStatus>
        <messageStatus>
            <status v="SUCCESSFULLY_SENT"/>
            <changeTimestamp v="2017-11-13T15:52:41Z"/>
        </messageStatus>
        <messageStatus>
            <status v="DELIVERED"/>
            <changeTimestamp v="2017-11-13T15:52:42Z"/>
        </messageStatus>
        <messageStatus>
            <status v="RECEIVED"/>
            <changeTimestamp v="2017-11-13T15:52:42Z"/>
        </messageStatus>
   </statusHistory>

</edx:StatusDocument>
*/
            var bytes = Convert.FromBase64String("PGVkeDpTdGF0dXNEb2N1bWVudCB4bWxuczplZHg9ImVkeC5lbnRzb2UuZXUuU3RhdHVzRG9jdW1lbnQueHNkIiB4bWxuczpjb21tb249ImVkeC5lbnRzb2UuZXUuY29tbW9uLnhzZCI+DQogICAgPGZpbmFsTWVzc2FnZVN0YXR1cz4NCiAgICAgICAgPHN0YXR1cyB2PSJSRUNFSVZFRCIvPg0KICAgICAgICA8Y2hhbmdlVGltZXN0YW1wIHY9IjIwMTctMTEtMTNUMTU6NTI6NDJaIi8+DQogICAgPC9maW5hbE1lc3NhZ2VTdGF0dXM+DQoNCiAgICA8ZWR4UmVwbHlNZXRhZGF0YT4NCiAgICAgICAgPG9yaWdpbmFsbWVzc2FnZUlEIHY9ImZjMTU0NmZmLTZlYTctNDBlNS04OWExLTgzOWU4YTVhYTE4ZiIvPg0KICAgICAgICA8cmVjZWl2ZVRpbWVzdGFtcCB2PSIyMDE3LTExLTEzVDE1OjUyOjQwWiIvPg0KICAgIDwvZWR4UmVwbHlNZXRhZGF0YT4NCg0KICAgIDxzdGF0dXNIaXN0b3J5Pg0KICAgICAgICA8bWVzc2FnZVN0YXR1cz4NCiAgICAgICAgICAgIDxzdGF0dXMgdj0iQUNDRVBURUQiLz4NCiAgICAgICAgICAgIDxjaGFuZ2VUaW1lc3RhbXAgdj0iMjAxNy0xMS0xM1QxNTo1Mjo0MFoiLz4NCiAgICAgICAgPC9tZXNzYWdlU3RhdHVzPg0KICAgICAgICA8bWVzc2FnZVN0YXR1cz4NCiAgICAgICAgICAgIDxzdGF0dXMgdj0iU0VOVCIvPg0KICAgICAgICAgICAgPGNoYW5nZVRpbWVzdGFtcCB2PSIyMDE3LTExLTEzVDE1OjUyOjQxWiIvPg0KICAgICAgICA8L21lc3NhZ2VTdGF0dXM+DQogICAgICAgIDxtZXNzYWdlU3RhdHVzPg0KICAgICAgICAgICAgPHN0YXR1cyB2PSJTVUNDRVNTRlVMTFlfU0VOVCIvPg0KICAgICAgICAgICAgPGNoYW5nZVRpbWVzdGFtcCB2PSIyMDE3LTExLTEzVDE1OjUyOjQxWiIvPg0KICAgICAgICA8L21lc3NhZ2VTdGF0dXM+DQogICAgICAgIDxtZXNzYWdlU3RhdHVzPg0KICAgICAgICAgICAgPHN0YXR1cyB2PSJERUxJVkVSRUQiLz4NCiAgICAgICAgICAgIDxjaGFuZ2VUaW1lc3RhbXAgdj0iMjAxNy0xMS0xM1QxNTo1Mjo0MloiLz4NCiAgICAgICAgPC9tZXNzYWdlU3RhdHVzPg0KICAgICAgICA8bWVzc2FnZVN0YXR1cz4NCiAgICAgICAgICAgIDxzdGF0dXMgdj0iUkVDRUlWRUQiLz4NCiAgICAgICAgICAgIDxjaGFuZ2VUaW1lc3RhbXAgdj0iMjAxNy0xMS0xM1QxNTo1Mjo0MloiLz4NCiAgICAgICAgPC9tZXNzYWdlU3RhdHVzPg0KICAgPC9zdGF0dXNIaXN0b3J5Pg0KDQo8L2VkeDpTdGF0dXNEb2N1bWVudD4=");

            var message = new Message(bytes);
            return message;
        }        
    }   
}