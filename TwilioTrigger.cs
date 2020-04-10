using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
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
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
           ILogger log,
           ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var bcConfig = new ConnectorConfig(config);

            BusinessCentraConnector centraConnector = new BusinessCentraConnector(bcConfig);
            var orders = await centraConnector.GetOrderByNumber("20NB-ORD010");
            var response = new MessagingResponse().Message(orders.Value[0].CustomerName);

            var orderDescription = response.ToString();

            return new HttpResponseMessage
            {
                Content = new StringContent(orderDescription, Encoding.UTF8, "application/xml")
            };
        }

    }
}
