using System;
using System.Configuration;
using System.Threading.Tasks;
using Amqp;
using Newtonsoft.Json;
using Statnett.EdxLib;

namespace Examples
{
    internal class ReplyReceiver
    {
        private static void Main(string[] args)
        {
            // Set up connection settings
            SendMessage().Wait();
        }

        private static async Task SendMessage()
        {
            var edxUrl = ConfigurationManager.AppSettings["EdxUrl"];

            var readQueue = ConfigurationManager.AppSettings["EdxOutboxReplyQueue"];

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
                Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented));
                Console.WriteLine(message.DecodeBodyAsString());
            }
        }
    }
}