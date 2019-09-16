using Newtonsoft.Json;

public class Player
{
        [JsonProperty("playerId")]
        public int PlayerId { get; }

        [JsonProperty("displayName")]
        public string DisplayName { get; }

        [JsonProperty("herdId")]
        public string HerdId { get; set; }

        [JsonProperty("magic")]
        public int Magic { get; set; }

        [JsonProperty("experience")]
        public PlayerExperience Experience { get; set; }
}