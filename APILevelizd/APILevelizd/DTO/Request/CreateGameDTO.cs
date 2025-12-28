using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using APILevelizd.Models.Enums;

namespace APILevelizd.DTO.Request;

public class CreateGameDTO
{
    public int GameId { get; set; }

    [Required]
    [StringLength(60)]
    public string? Name { get; set; }

    [Required]
    public string YearOfRelease { get; set; }
    
    [Required]
    public IFormFile? GameImage { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Genres Genre { get; set; }
}
