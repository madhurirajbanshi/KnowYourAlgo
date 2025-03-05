namespace API.Dtos.Algorithm;

public class AlgorithmCreateDto
{
    public string Title { get; set; }
    public int CourseId { get; set; }
    public ICollection<int> TagIds { get; set; }
}
