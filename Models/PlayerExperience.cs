using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerExperience
{
    [JsonProperty("allowedTokens")]
    public List<TokenType> AllowedTokens { get; set; }

    [JsonProperty("unlockedAreas")]
    public List<Area> UnlockedAreas{ get; set; }
}

