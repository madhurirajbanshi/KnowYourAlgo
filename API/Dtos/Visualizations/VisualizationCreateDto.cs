namespace API.Dtos.Visualizations;

public class VisualizationCreateDto
{
    public string Html { get; set; }
    public string Css { get; set; }
    public string Js { get; set; }
    public string Title { get; set; }
    public int AlgorithmId { get; set; }
}
