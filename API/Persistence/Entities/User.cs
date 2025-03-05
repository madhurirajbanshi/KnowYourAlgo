namespace API.Persistence.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Visualization> Visualizations { get; set; } = new List<Visualization>();
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
