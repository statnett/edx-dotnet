using System;
using System.Configuration;
using Amqp;
using Statnett.EdxLib;

namespace Examples
{
    internal class Sender
    {
        /// <summary>
        /// Simple example building a message and sending it.
        /// 
        /// Note: Uses synchronous methods for handling connections, AMQP Lite recommends using Async methods
        /// </summary>
        private static void Main()
        {
            // Set up connection settings
            var edxUrl = ConfigurationManager.AppSettings["EdxUrl"];
            var edxOutbox = ConfigurationManager.AppSettings["EdxOutboxQueue"];
            var connection = new Connection(new Address(edxUrl));
            var session = new Session(connection);
            var outbox = new SenderLink(session, "outbox", edxOutbox);

            // App settings            
            var timeout = int.Parse(ConfigurationManager.AppSettings["TimeoutMs"]);

            // Create message
            const string content = "Hello world";

            var msgId = Guid.NewGuid();
            var msg = FluentMessageBuilder
                .CreateMessage("myBusinessType", content)
                .WithReceiverAddress("endpoint@SERVICE-MYSERVICE")
                .WithBusinessMessageId(msgId)
                .WithCorrelationId(Guid.NewGuid())
                .WithSenderApplication("MyAppName");

            try
            {
                // Send message
                Console.WriteLine($"Sending message {content} to {edxUrl} ...");

                outbox.Send(msg, TimeSpan.FromMilliseconds(timeout));

                Console.WriteLine("Success!");
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine(ex);
            }
            finally
            {
                // Close all connections
                outbox.Close();
                session.Close();
                connection.Close();
            }
        }
    }
}