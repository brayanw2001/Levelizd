using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILevelizd.Models;

public class Review
{
    public int ReviewId { get; set; }
    [Required]
    public bool IsPlayed { get; set; }
    [Column(TypeName = "decimal(2, 1)")]
    public decimal Rating { get; set; }
    public string? Comment { get; set; }
    public Game Game { get; set; }
    public User User { get; set; }
}
