namespace APILevelizd.Models;

public class Review
{
    public int Id { get; set; }
    public float Rating { get; set; }
    public string? Comment { get; set; }
    public Game? Game { get; set; }
}
