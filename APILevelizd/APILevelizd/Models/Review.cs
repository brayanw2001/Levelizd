using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APILevelizd.Models;

public class Review
{
    public int ReviewId { get; set; }

    [Required]
    public bool IsPlayed { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal Rating { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    [Required]
    public int GameId { get; set; }
    [JsonIgnore]
    public virtual Game? Game { get; set; }

    [Required]
    public int UserId { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }
}
