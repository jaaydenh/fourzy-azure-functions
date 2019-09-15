using Newtonsoft.Json;

public class Player
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Player1 { get; set; }
    public string Player2 { get; set; }
}