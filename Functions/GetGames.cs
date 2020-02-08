using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public static class GetGames
    {
        [FunctionName("GetGames")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "fourzy", collectionName: "games", ConnectionStringSetting = "fourzyConnection")] DocumentClient client,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string playerId = req.Query["playerId"];
        
            Uri gameCollectionUri = UriFactory.CreateDocumentCollectionUri(databaseId: "fourzy", collectionId: "games");
        
            var options = new FeedOptions { EnableCrossPartitionQuery = true }; // Enable cross partition query
        
            IDocumentQuery<Game> query = client.CreateDocumentQuery<Game>(gameCollectionUri, options)
                                                .Where(game => game.InitialGameStateData.Players[0].PlayerString == playerId)
                                                .AsDocumentQuery();
        
            var gamesForStore = new List<Game>();
        
            while (query.HasMoreResults)
            {
                foreach (Game driver in await query.ExecuteNextAsync())
                {
                    gamesForStore.Add(driver);
                }
            }                       
        
            return gamesForStore.Count > 0
                ? (ActionResult)new OkObjectResult(gamesForStore)
                : new BadRequestObjectResult("No games were returned");
        }
    }
}
