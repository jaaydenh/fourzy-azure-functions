using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public static class PlayFabHelpers
    {
        private static readonly HttpClient client;
        const string titleId = "9EB47";
        const string getPlayerProfileUrl = "https://9EB47.playfabapi.com/Server/GetPlayerProfile";
        const string secretKey = "8F65ZGJU93HAB6PTQGXKX7OJ94ZKIB3GZ35OCTAAFAC6GN6QNC";
        

        static PlayFabHelpers()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-SecretKey", secretKey);
        }

        public static async Task<string> GetPlayerProfile(string PlayFabId, CancellationToken token)
        {
            var my_jsondata = new
            {
                PlayFabId = PlayFabId
            };
            
            var json = JsonConvert.SerializeObject(my_jsondata);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await client.PostAsync(getPlayerProfileUrl, stringContent, token);
            
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.StatusCode + " : " + response.ReasonPhrase);
            }

            return content;
        }
    }
}