using System.ComponentModel.DataAnnotations;

namespace APILevelizd.Models;

public class Review
{
    public int ReviewId { get; set; }
    [Required]
    public bool IsPlayed { get; set; }
    public float Rating { get; set; }
    public string? Comment { get; set; }
    public Game Game { get; set; }
    public User User { get; set; }
}
