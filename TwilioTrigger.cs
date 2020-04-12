using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Twilio.TwiML;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace com.businesscentral
{
    public static class TwilioTrigger
    {
        [FunctionName("TwilioConnector")]
        public static async Task<HttpResponseMessage> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] 
           HttpRequestMessage req,
           ILogger log,
           ExecutionContext context)
        {
            // Load configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Incoming whatsapp message is parsed
            var requestMessage = await req.Content.ReadAsStringAsync();
            InputMessage parser= new InputMessage(requestMessage);

            // Business Central is queried
            var bcConfig = new ConnectorConfig(config);
            BusinessCentralConnector centraConnector = new BusinessCentralConnector(bcConfig);
            var orders = await centraConnector.GetOrderByNumber(parser.MessageOrderNumber);

            // Outcoming whatsapp message is composed
            MessageComposer composer = new MessageComposer();
            string replyText=composer.DataBindMessage(orders,parser);
            
            // Outcoming whatsapp message is replied back
             var response = new MessagingResponse().Message(replyText);
            return new HttpResponseMessage
            {
                Content = new StringContent(response.ToString(), Encoding.UTF8, "application/xml")
            };
        }

       

    }
}
