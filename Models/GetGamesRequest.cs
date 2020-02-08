using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public class GetGamesRequest
    {
        [JsonProperty("playerId")]
        public string PlayerId { get; set; }
    }
}
