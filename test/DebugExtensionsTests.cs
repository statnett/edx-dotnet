using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Statnett.EdxLib.Tests
{
    [TestClass]
    public class DebugExtensionsTests
    {
        private const string Expected = 
@"Message content:
- Properties:
  > ContentType:	application/octetstream
  > MessageId:		1b0a9b7b-92e6-4f2a-a2d2-8bc679ffbdd4
- ApplicationProperties:
  > businessType:	BusinessTypeBusinessType
";

        [TestMethod]
        public void ToVerboseString_CreatesRichOutput()
        {
            var message = FluentMessageBuilder.CreateMessage("BusinessType", "Hello World!");
            message.Properties.MessageId = "1b0a9b7b-92e6-4f2a-a2d2-8bc679ffbdd4";
            var result = message.ToVerboseString();

            Assert.AreEqual(Expected, result);
        }
    }
}
