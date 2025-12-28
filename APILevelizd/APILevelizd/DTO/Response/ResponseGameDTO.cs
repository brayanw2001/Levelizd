using APILevelizd.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APILevelizd.DTO.Response;

public class ResponseGameDTO
{
    public int GameId { get; set; }

    [Required]
    [StringLength(60)]
    public string? Name { get; set; }

    [Required]
    public string YearOfRelease { get; set; }

    [Required]
    public string GameImageUrl { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Genres Genre { get; set; }
}
