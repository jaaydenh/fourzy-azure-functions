using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using FourzyGameModel;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public static class CreateGame
    {
        [FunctionName("CreateGame")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req, 
            [CosmosDB(databaseName: "fourzy", collectionName: "games", ConnectionStringSetting = "fourzyConnection")] ICollector<Game> game,
            ILogger log,
            CancellationToken token)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a CreateGame request.");
            
                // Inputs: playerid, area, opponentId (if no opponentId, this will trigger the matchmaker)
                // Outputs: GameState, FirstPlayerId

                string reqContent = req.Content.ReadAsStringAsync().Result;

                CreateGameRequest createGameReq = JsonConvert.DeserializeObject<CreateGameRequest>(reqContent);

                // TODO: Get player display name from Playfab

                // Create Player
                FourzyGameModel.Model.Player player = new FourzyGameModel.Model.Player();
                player.PlayerId = 1;
                player.PlayerString = createGameReq.PlayerId;
                player.DisplayName = "Player One Test";
                player.HerdId = "1";
                player.Magic = 100;
                player.SelectedArea = Area.TRAINING_GARDEN;
                FourzyGameModel.Model.PlayerExperience pex1 = new FourzyGameModel.Model.PlayerExperience();
                player.Experience = pex1;

                // Create Opponent
                var opponentID = createGameReq.OpponentId;
                // Get Opponent from Matchmaker if CreateGame is called without an opponent
                if (createGameReq.OpponentId == null)
                {
                    // TODO: GetOpponent from Matchmaker
                    // opponentid = GetOpponent(playerId, area);
                }
                // TODO: Get opponent info from Playfab

                FourzyGameModel.Model.Player opponent = new FourzyGameModel.Model.Player();
                opponent.PlayerId = 2;
                opponent.PlayerString = createGameReq.OpponentId;
                opponent.DisplayName = "Player Two Test";
                opponent.HerdId = "1";
                opponent.Magic = 100;
                opponent.SelectedArea = Area.TRAINING_GARDEN;
                FourzyGameModel.Model.PlayerExperience pex2 = new FourzyGameModel.Model.PlayerExperience();
                opponent.Experience = pex2;
                
                // Assign Player and Opponent
                FourzyGameModel.Model.CreateGameRequest request = new FourzyGameModel.Model.CreateGameRequest();
                request.Player = player;
                request.Opponent = opponent;

                string gameStateData = await CreateGameHelpers.CreateGameState(request, token);

                log.LogInformation("gameStateData: " + gameStateData.ToString());

                var newGame = new Game();
                newGame.GameStateData = gameStateData;
                newGame.PlayerTurnRecord = new List<string> ();
                newGame.FirstPlayerId = "0";

                if (gameStateData != String.Empty)
                {
                    game.Add(newGame);
                    return (ActionResult)new OkObjectResult(newGame);
                } else {
                    return new BadRequestObjectResult("Error Creating GameState");
                }
            }
            catch (TaskCanceledException taskEx)
            {
                log.LogInformation("Create Game Cancelled: " + taskEx.ToString());

                return new BadRequestObjectResult("Create Game Cancelled");
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
