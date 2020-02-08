using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public class CreateGameResponse
    {
        [JsonProperty("game")]
        public Game Game { get; set; }
    }
}
