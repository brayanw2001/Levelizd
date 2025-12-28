using APILevelizd.Models;
using System.ComponentModel.DataAnnotations;

namespace APILevelizd.DTO.Request;

public class CreateUserDTO
{
    public int UserId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    //[Required]    
    public string Password { get; set; }

    //[Required]    
    public IFormFile? UserImage { get; set; }

}
