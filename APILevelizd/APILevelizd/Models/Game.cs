using System.ComponentModel.DataAnnotations;
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

    [Required]
    [StringLength(60)]
    public string? Name { get; set; }

    [Required]
    public DateTime YearOfRelease { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Genres Genre { get; set; }

    [JsonIgnore]
    public virtual ICollection<Review>? Reviews { get; set; }            // um game vai ter várias reviews
}