namespace API.Dtos.Visualizations;

public class VisualizationDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string UserName { get; set; }
    public int UserId { get; set; }
    public string Html { get; set; }
    public string Css { get; set; }
    public string Js { get; set; }
    public long Views { get; set; }
    public long VoteCount { get; set; }
    public bool IsVoted { get; set; }
    public decimal TrendScore { get; set; }
    public string Algorithm { get; set; }
}
