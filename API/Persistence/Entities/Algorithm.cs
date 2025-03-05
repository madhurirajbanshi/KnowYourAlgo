namespace API.Persistence.Entities;

public class Algorithm
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Visualization> Visualizations { get; set; } = [];
}
