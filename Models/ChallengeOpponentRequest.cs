using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public class ChallengeOpponentRequest
    {
        [JsonProperty("playerId")]
        public string PlayerId { get; set; }

        [JsonProperty("opponentId")]
        public string OpponentId { get; set; }
    }
}
