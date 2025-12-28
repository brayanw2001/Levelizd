using APILevelizd.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APILevelizd.DTO.Request;

public class CreateReviewDTO
{
    public int ReviewId { get; set; }

    [Required]
    public bool IsPlayed { get; set; }

    public decimal Rating { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    [Required]
    public int GameId { get; set; }

    [Required]
    public int UserId { get; set; }
}
