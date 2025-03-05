namespace API.Persistence.Entities;

public class Vote
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime VotedAt { get; set; } = DateTime.UtcNow;

}
