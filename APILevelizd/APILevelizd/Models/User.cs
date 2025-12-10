namespace APILevelizd.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }       // ...public?
    public ICollection<Review> Reviews { get; set; }

    // adicionar foto futuramente
}
