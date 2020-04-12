using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace com.businesscentral
{
    public class BusinessCentralConnector
    {
        ConnectorConfig config;
        public BusinessCentralConnector(ConnectorConfig config)
        {
            this.config = config;
        }
        public async Task<SalesOrders> GetOrderByNumber(string salesOrderNumber)
        {
            SalesOrders orders = null;

            using (var httpClient = new HttpClient())
            {
                var apiEndPoint = String.Format("https://api.businesscentral.dynamics.com/{0}/{1}/api/{2}/companies({3})/",
                                    config.apiVersion1, config.tenant, config.apiVersion2, config.companyID);
                var query = String.Format("salesOrders?$filter=number eq '{0}'", salesOrderNumber);
                var baseUrl = apiEndPoint + query;

                String authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(config.authInfo));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);

                var responseMessage = await httpClient.GetAsync(baseUrl);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonContent = await responseMessage.Content.ReadAsStringAsync();
                    orders = JsonConvert.DeserializeObject<SalesOrders>(jsonContent);

                }
            }
            return orders;
        }
    }

}
