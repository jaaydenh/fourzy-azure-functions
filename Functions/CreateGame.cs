using System;
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

namespace Fourzy
{
    public static class CreateGame
    {
        [FunctionName("CreateGame")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestMessage req,
            [CosmosDB(databaseName: "fourzy", collectionName: "games", ConnectionStringSetting = "fourzyConnection")] out Game game,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Inputs: playerid, area, optional: opponent playerid
            // Outputs: Gameboard, player to play first

            // string name = req.Query["name"];

            // Get player info from Playfab player table

            // Call matchmaking with playerid
            // Matchmaking returns
            // Opponent playerid
            // Gameboard - the Gameboard is generated

            // get opponent info from Playfab


            
            var content = req.Content; 
            string jsonContent = content.ReadAsStringAsync().Result; 
            game = JsonConvert.DeserializeObject<Game>(jsonContent);
            // game = new Game {Board="TEST",Player1="player1", Player2="player2"};
            
            return new HttpResponseMessage(HttpStatusCode.Created);

            // return name != null
            //     ? (ActionResult)new OkObjectResult($"Hello, {name}")
            //     : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        public static string Matchmaking(string playerId, string area) 
        {
            // if player does not exist in matchmaking pool and turn-based games optin is on, add player to pool
            
            // last turn is within the last 7 days, lastTurnTimestamp
            // player optin to turn-based games is true
            // get player with oldest lastGameCreatedTimestamp

            var opponentID = "";

            return opponentID;

            // generate gameboard from game model service
        }

        public static void CreateGameState()
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
        }
    }
}
