using Newtonsoft.Json;

public class Game
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string Board { get; set; }
    public string Player1 { get; set; }
    public string Player2 { get; set; }
}