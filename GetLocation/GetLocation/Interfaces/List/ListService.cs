using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GetLocation.Interfaces.List
{
    public class ListService : IListService
    {
        public ListService()
        {
        }

        public async Task<ListReponse> GetList(string accessToken)
        {
            ListReponse listReponse = new ListReponse();
            HttpClient _client;

            var authData = string.Format("{0}:{1}", accessToken, "");

            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            Uri url = new Uri("https://genesys.topcall.com.vn/web/webservice/get-list-record-info");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = url,
            };
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                // convert reponse to string json
                var reponseString = await response.Content.ReadAsStringAsync();
                // convert string json to object
                listReponse = JsonConvert.DeserializeObject<ListReponse>(reponseString);
            }
            return listReponse;
        }
    }
}
