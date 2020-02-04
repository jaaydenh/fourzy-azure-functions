using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public class CreateGameRequest
    {
        [JsonProperty("playerId")]
        public string PlayerId { get; set; }

        [JsonProperty("opponentId")]
        public string OpponentId { get; set; }
                
        [JsonProperty("area")]
        public Area Area { get; set; }
    }
}
