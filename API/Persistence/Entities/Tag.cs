namespace API.Persistence.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Algorithm> Algorithms { get; set; } = [];
}
