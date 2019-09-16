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
        public static object Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestMessage req,
            [CosmosDB(databaseName: "fourzy", collectionName: "games", ConnectionStringSetting = "fourzyConnection")] out Game game,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a CreateGame request.");

            // Inputs: playerid, area, optional: opponent playerid
            // Outputs: Gameboard, player to play first

            // string playerid = req.Query["playerid"];
            // string area = req.Query["area"];
            // string opponentid = req.Query["opponentid"];

            // TODO: Get player display name from Playfab

            // // Get Opponent from Matchmaker if CreateGame is called without an opponent
            // if (opponentid == null)
            // {
            //     opponentid = GetOpponent(playerId, area);
            // }
            
            // TODO: Get opponent display name from Playfab

            string gameStateData = CreateGameState(new Player(), new Player());

            var content = req.Content;
            game = new Game();
            // game.Id = "0";
            game.GameStateData = content.ReadAsStringAsync().Result;
            game.GameStateDataCurrent = content.ReadAsStringAsync().Result;
            game.PlayerTurnRecord = new List<string> ();
            game.FirstPlayerId = "0";

            // game = JsonConvert.DeserializeObject<Game>(jsonContent);

            if (game != null)
            {
                return req.CreateResponse(HttpStatusCode.OK, game);
            } else {
                return req.CreateResponse(HttpStatusCode.BadRequest, new {
                    error = "Error creating game state"
                });
            }
            
            // return new HttpResponseMessage(HttpStatusCode.Created);
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

        public static string CreateGameState(Player player, Player opponent)
        {
            //***** Call gamemodel service -> createGame using info from both players *****

            // var gameStateData = "";
            // var createGameRequest = "";

            // var playerObj = {
            //     playerId:"1", 
            //     playerString:player.playerId, 
            //     displayName:player.displayName,
            //     herdId:player.herdId,
            //     magic:player.magic,
            //     selectedArea:area,
            //     experience:{"allowedTokens":[],"unlockedAreas":["2"]}
            // };
            // createGameRequest.player = playerObj;
            // var opponentObj = {
            //     playerId:"2", 
            //     playerString:opponent.playerId, 
            //     displayName:opponent.displayName,
            //     herdId:opponent.herdId,
            //     magic:opponent.magic,
            //     selectedArea:opponent.selectedArea,
            //     experience:{"allowedTokens":[],"unlockedAreas":["2"]}
            // };
            // createGameRequest.opponent = opponentObj;

            // var url = "http://fourzygamemodelserviceapi.azurewebsites.net/api/creategame";
            // var headers = {"Content-Type":"application/json; charset=utf-8"};
            // var response = Spark.getHttp(url).setHeaders(headers).postJson(createGameRequest);
            // var responseJson = response.getResponseJson();

            // gameStateData = responseJson.gameStateData;

            return "{'placeholder gameStateData'}";
        }
    }
}
