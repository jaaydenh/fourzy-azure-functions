using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public static class CreateGameHelpers
    {
        private static HttpClient client = new HttpClient();
        const string url = "https://fourzygamemodelserviceapi.azurewebsites.net/api/creategame";

        public static async Task<string> CreateGameState(string createGameReq )
        {
            // var json = JsonConvert.SerializeObject(createGameReq);
            var stringContent = new StringContent(createGameReq, UnicodeEncoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent);
            
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.StatusCode + " : " + response.ReasonPhrase);
            }

            return content;
        }
    }
}