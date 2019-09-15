using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Fourzy
{
    public static class GameUpdated
    {
        [FunctionName("GameUpdated")]
        public static void Run([CosmosDBTrigger(
            databaseName: "fourzy",
            collectionName: "games",
            ConnectionStringSetting = "fourzyConnection",
            CreateLeaseCollectionIfNotExists=true,
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
