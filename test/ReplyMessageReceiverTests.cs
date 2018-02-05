using System;
using Amqp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Statnett.EdxLib.Tests
{
    [TestClass]
    public class ReplyMessageReceiverTests
    {
        [TestMethod]
        public void Test()
        {
            var message = CreateErrorMessage();

            Console.WriteLine(message.DecodeBodyAsMessageStatus());

        }

        private static Message CreateErrorMessage()
        {
            var bytes = Convert.FromBase64String("PGVkeDpTdGF0dXNEb2N1bWVudCB4bWxuczplZHg9ImVkeC5lbnRzb2UuZXUuU3RhdHVzRG9jdW1lbnQueHNkIiB4bWxuczpjb21tb249ImVkeC5lbnRzb2UuZXUuY29tbW9uLnhzZCI+CiAgICA8ZmluYWxNZXNzYWdlU3RhdHVzPgogICAgICAgIDxzdGF0dXMgdj0iRkFJTEVEIi8+CiAgICAgICAgPGNoYW5nZVRpbWVzdGFtcCB2PSIyMDE4LTAyLTA1VDE3OjA1OjI2WiIvPgogICAgICAgIDxzdGF0dXNUZXh0IHY9IlRoaXMgdG9vbGJveCBoYXMgbm8gcmVsYXRpb24gdG8gdGhlIHNlcnZpY2UgTVlTRVJWSUNFLiIvPgogICAgPC9maW5hbE1lc3NhZ2VTdGF0dXM+CgogICAgPGVkeFJlcGx5TWV0YWRhdGE+CiAgICAgICAgPHJlY2VpdmVUaW1lc3RhbXAgdj0iMjAxOC0wMi0wNVQxNzowNToyNloiLz4KICAgIDwvZWR4UmVwbHlNZXRhZGF0YT4KCiAgICA8c3RhdHVzSGlzdG9yeT4KICAgICAgICA8bWVzc2FnZVN0YXR1cz4KICAgICAgICAgICAgPHN0YXR1cyB2PSJGQUlMRUQiLz4KICAgICAgICAgICAgPGNoYW5nZVRpbWVzdGFtcCB2PSIyMDE4LTAyLTA1VDE3OjA1OjI2WiIvPgogICAgICAgICAgICA8c3RhdHVzVGV4dCB2PSJUaGlzIHRvb2xib3ggaGFzIG5vIHJlbGF0aW9uIHRvIHRoZSBzZXJ2aWNlIE1ZU0VSVklDRS4iLz4KICAgICAgICA8L21lc3NhZ2VTdGF0dXM+CiAgICA8L3N0YXR1c0hpc3Rvcnk+Cgo8L2VkeDpTdGF0dXNEb2N1bWVudD4K");

            var message = new Message(bytes);
            return message;
        }


        public const string ErrorResult = "";
    }
}
