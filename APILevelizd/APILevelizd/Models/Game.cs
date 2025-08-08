using System.Text.Json.Serialization;

namespace APILevelizd.Models;

public class Game
{
    public enum Genres
    {
        FPS,
        RPG,
        JRPG,
    }
    
    public int GameId { get; set; }
    public string? Name { get; set; }
    public DateTime YearOfRelease { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Genres Genre { get; set; }

    public ICollection<Review> Reviews { get; set; }            // um game vai ter várias reviews
}
