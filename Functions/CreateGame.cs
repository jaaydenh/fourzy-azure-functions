using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public static class CreateGame
    {
        [FunctionName("CreateGame")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestMessage req,
            [CosmosDB(databaseName: "fourzy", collectionName: "games", ConnectionStringSetting = "fourzyConnection")] ICollector<Game> game,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a CreateGame request.");
            
                // Inputs: playerid, area, optional: opponent playerid
                // Outputs: Gameboard, player to play first

                var reqContent = req.Content.ReadAsStringAsync().Result;

                // var createGameRequest = JsonConvert.DeserializeObject<CreateGameRequest>(reqContent);

                // TODO: Get player display name from Playfab

                // Get Opponent from Matchmaker if CreateGame is called without an opponent
                // if (opponentid == null)
                // {
                //     opponentid = GetOpponent(playerId, area);
                // }
                
                // TODO: Get opponent display name from Playfab

                string gameStateData = await CreateGameHelpers.CreateGameState(reqContent);

                log.LogInformation("gameStateData: " + gameStateData.ToString());

                var newGame = new Game();
                newGame.GameStateData = gameStateData;
                newGame.PlayerTurnRecord = new List<string> ();
                newGame.FirstPlayerId = "0";

                game.Add(newGame);

                if (game != null)
                {
                    return (ActionResult)new OkObjectResult(newGame);
                } else {
                    return new BadRequestObjectResult("Error Creating GameState");
                }
            }
            catch (System.Exception ex)
            {
                log.LogError("Error Creating GameState: " + ex.ToString());
 
                return new BadRequestObjectResult("Error Creating GameState");
            } 
        }

        public static string GetOpponent(string playerId, string area) 
        {
            // TODO: Create a collection in CosmosDb for the matchmaking pool
            // if player does not exist in matchmaking pool and turn-based games optin is on, add player to pool
            
            // last turn is within the last 7 days, lastTurnTimestamp
            // player optin to turn-based games is true
            // get player with oldest lastGameCreatedTimestamp

            var opponentID = "";

            return opponentID;
        } 
    }
}
