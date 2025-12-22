using APILevelizd.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APILevelizd.DTO
{
    public class ReviewDTO
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
}
