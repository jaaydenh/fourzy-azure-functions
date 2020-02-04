using Newtonsoft.Json;

namespace FourzyGameModel.Model
{
    public class Player
    {
            [JsonProperty("playerId")]
            public int PlayerId { get; set; }

            [JsonProperty("playerString")]
            public string PlayerString { get; set; }

            [JsonProperty("displayName")]
            public string DisplayName { get; set;}

            [JsonProperty("herdId")]
            public string HerdId { get; set; }

            [JsonProperty("magic")]
            public int Magic { get; set; }

            [JsonProperty("selectedArea")]
            public Area SelectedArea { get; set; }

            [JsonProperty("experience")]
            public PlayerExperience Experience { get; set; }
    }
}