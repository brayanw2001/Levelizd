using System.ComponentModel.DataAnnotations;

namespace APILevelizd.Models;

public class User
{
    public int UserId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    //[Required]    
    public string Senha { get; set; }       // ...public?
    public ICollection<Review>? Reviews { get; }

    // adicionar foto futuramente
}
