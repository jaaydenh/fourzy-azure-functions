using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public static class CreateGameHelpers
    {
        private static readonly HttpClient client;
        const string url = "https://fourzygamemodelserviceapi.azurewebsites.net/api/creategame";

        static CreateGameHelpers()
        {
            client = new HttpClient();
        }

        public static async Task<string> CreateGameState(FourzyGameModel.Model.CreateGameRequest createGameReq, CancellationToken token)
        {
            var json = JsonConvert.SerializeObject(createGameReq);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent, token);
            
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.StatusCode + " : " + response.ReasonPhrase);
            }

            return content;
        }
    }
}