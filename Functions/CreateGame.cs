using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FourzyGameModel.Model;

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
            
                // Inputs: playerid
                // Outputs: GameState

                string reqContent = req.Content.ReadAsStringAsync().Result;

                CreateGameRequest createGameRequest = JsonConvert.DeserializeObject<CreateGameRequest>(reqContent);
                
                log.LogInformation("PlayFab Player Profile: " + PlayFabHelpers.GetPlayerProfile(createGameRequest.PlayerId, token));

                // Assign Player and Opponent
                FourzyGameModel.Model.CreateGameRequest request = new FourzyGameModel.Model.CreateGameRequest();
                // TODO: Get both players displaynames from Playfab
                request.Player = CreateMockPlayer(1, createGameRequest.PlayerId, "Player One Test");;
                var opponentID = GetOpponent(createGameRequest.PlayerId, Area.TRAINING_GARDEN);
                request.Opponent = CreateMockPlayer(2, opponentID, "Player Two Test");

                GameStateData gameStateData = await CreateGameHelpers.CreateGameState(request, token);

                // log.LogInformation("gameStateData: " + gameStateData.ToString());

                var newGame = new Game();
                newGame.InitialGameStateData = gameStateData;
                newGame.CurrentGameStateData = gameStateData;
                newGame.PlayerTurnRecord = new List<PlayerTurn> ();

                CreateGameResponse response = new CreateGameResponse();
                response.Game  = newGame; 

                if (gameStateData != null)
                {
                    game.Add(newGame);
                    return (ActionResult)new OkObjectResult(response.Game);
                } else {
                    return new BadRequestObjectResult("Error Retrieving Initial GameStateData");
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
 
                return new BadRequestObjectResult("Error Retrieving Initial GameStateData");
            } 
        }

        public static string GetOpponent(string playerId, Area area) 
        {
            // TODO: Create a collection in CosmosDb for the matchmaking pool
            // if player does not exist in matchmaking pool and turn-based games optin is on, add player to pool
            
            // last turn is within the last 7 days, lastTurnTimestamp
            // player optin to turn-based games is true
            // get player with oldest lastGameCreatedTimestamp

            var opponentID = "5372937FA3E72CF8";

            return opponentID;
        } 

        public static Player CreateMockPlayer(int playerIndex, string playerId, string displayName) {
            Player player = new Player(playerIndex, displayName);
            player.PlayerString = playerId;
            player.HerdId = "1";
            player.Magic = 100;
            player.SelectedArea = Area.TRAINING_GARDEN;
            PlayerExperience pex2 = new PlayerExperience();
            player.Experience = pex2;

            return player;
        }
    }
}
