using System;
using Amqp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statnett.EdxLib.ModelExtensions;

namespace Statnett.EdxLib.Tests
{
    [TestClass]
    public class ReplyMessageReceiverTests
    {
        [TestMethod]
        public void CanReadFailingMessage()
        {
            var expected = new MessageResult
            {
                FinalMessageStatus = ExpectedFinalErrorMessage
            };         

            var result = CreateErrorMessage().DecodeBodyAsMessageStatus();

            Assert.AreEqual(expected, result);
        }

        private static MessageStatus ExpectedFinalErrorMessage
        {
            get
            {
                var expectedFinalMessage = new MessageStatus
                {
                    ChangeTimeStamp = new DateTime(2018, 02, 05, 17, 05, 26, DateTimeKind.Utc),
                    Status = Status.Failed,
                    StatusText = "This toolbox has no relation to the service MYSERVICE."
                };
                return expectedFinalMessage;
            }
        }

        private static Message CreateErrorMessage()
        {
            /*
<edx:StatusDocument xmlns:common="edx.entsoe.eu.common.xsd" xmlns:edx="edx.entsoe.eu.StatusDocument.xsd">
	<finalMessageStatus>
		<status v="FAILED"/>
		<changeTimestamp v="2018-02-05T17:05:26Z"/>
		<statusText v="This toolbox has no relation to the service MYSERVICE."/>
	</finalMessageStatus>
	<edxReplyMetadata>
		<receiveTimestamp v="2018-02-05T17:05:26Z"/>
	</edxReplyMetadata>
	<statusHistory>
		<messageStatus>
			<status v="FAILED"/>
			<changeTimestamp v="2018-02-05T17:05:26Z"/>
			<statusText v="This toolbox has no relation to the service MYSERVICE."/>
		</messageStatus>
	</statusHistory>
</edx:StatusDocument>             */
            var bytes = Convert.FromBase64String("PGVkeDpTdGF0dXNEb2N1bWVudCB4bWxuczplZHg9ImVkeC5lbnRzb2UuZXUuU3RhdHVzRG9jdW1lbnQueHNkIiB4bWxuczpjb21tb249ImVkeC5lbnRzb2UuZXUuY29tbW9uLnhzZCI+CiAgICA8ZmluYWxNZXNzYWdlU3RhdHVzPgogICAgICAgIDxzdGF0dXMgdj0iRkFJTEVEIi8+CiAgICAgICAgPGNoYW5nZVRpbWVzdGFtcCB2PSIyMDE4LTAyLTA1VDE3OjA1OjI2WiIvPgogICAgICAgIDxzdGF0dXNUZXh0IHY9IlRoaXMgdG9vbGJveCBoYXMgbm8gcmVsYXRpb24gdG8gdGhlIHNlcnZpY2UgTVlTRVJWSUNFLiIvPgogICAgPC9maW5hbE1lc3NhZ2VTdGF0dXM+CgogICAgPGVkeFJlcGx5TWV0YWRhdGE+CiAgICAgICAgPHJlY2VpdmVUaW1lc3RhbXAgdj0iMjAxOC0wMi0wNVQxNzowNToyNloiLz4KICAgIDwvZWR4UmVwbHlNZXRhZGF0YT4KCiAgICA8c3RhdHVzSGlzdG9yeT4KICAgICAgICA8bWVzc2FnZVN0YXR1cz4KICAgICAgICAgICAgPHN0YXR1cyB2PSJGQUlMRUQiLz4KICAgICAgICAgICAgPGNoYW5nZVRpbWVzdGFtcCB2PSIyMDE4LTAyLTA1VDE3OjA1OjI2WiIvPgogICAgICAgICAgICA8c3RhdHVzVGV4dCB2PSJUaGlzIHRvb2xib3ggaGFzIG5vIHJlbGF0aW9uIHRvIHRoZSBzZXJ2aWNlIE1ZU0VSVklDRS4iLz4KICAgICAgICA8L21lc3NhZ2VTdGF0dXM+CiAgICA8L3N0YXR1c0hpc3Rvcnk+Cgo8L2VkeDpTdGF0dXNEb2N1bWVudD4K");

            var message = new Message(bytes);
            return message;
        }


        public const string ErrorResult = "";
    }
}
