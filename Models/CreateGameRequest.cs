using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public class CreateGameRequest
    {
        [JsonProperty("playerId")]
        public string PlayerId { get; set; }
    }
}
