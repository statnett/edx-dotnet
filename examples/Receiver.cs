using System;
using System.Configuration;
using Amqp;
using System.Threading.Tasks;
using Examples.Tools;

// ReSharper disable All

namespace SimpleReceiver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Set up connection settings
            SendMessage().Wait();
        }

        private static async Task SendMessage()
        {
            var edxUrl = ConfigurationManager.AppSettings["EdxUrl"];

            var readQueue = ConfigurationManager.AppSettings["EdxInboxQueue"]; // For reading normal input messages
            //var readQueue = ConfigurationManager.AppSettings["EdxOutboxReplyQueue"]; // For reading sent message status from EDX

            var connection = new Connection(new Address(edxUrl));
            var session = new Session(connection);
            var inbox = new ReceiverLink(session, "inbox", readQueue);

            // App settings            
            var timeout = int.Parse(ConfigurationManager.AppSettings["TimeoutMs"]);

            try
            {
                var result = await inbox.ReceiveAsync(TimeSpan.FromMilliseconds(timeout));

                ToConsole(result); // Print received message

                if (result != null)
                {
                    inbox.Accept(result);
                    Console.WriteLine("Message accepted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                // Close all connections
                await inbox.CloseAsync();
                await session.CloseAsync();
                await connection.CloseAsync();
            }
        }

        private static void ToConsole(Message message)
        {
            if (message == null)
            {
                Console.WriteLine("No message received...");
            }
            else
            {
                Console.WriteLine(message.ToVerboseString(true));
            }
        }
    }
}