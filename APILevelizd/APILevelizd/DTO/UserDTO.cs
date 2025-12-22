using APILevelizd.Models;
using System.ComponentModel.DataAnnotations;

namespace APILevelizd.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        //[Required]    
        public string Senha { get; set; }
    }
}
