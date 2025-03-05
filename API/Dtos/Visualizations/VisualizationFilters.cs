using System;
using System.Diagnostics.CodeAnalysis;

namespace API.Dtos.Visualizations;

public class VisualizationFilters
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool? IsTrending { get; set; }
    public long? LikeGreaterThan { get; set; }
    public long? LikeLessThan { get; set; }
    public long? ViewCountGreaterThan { get; set; }
    public long? ViewCountLessThan { get; set; }
    public long? AlgorithmId { get; set; }
    public bool? IsViewsDecending { get; set; }
    public bool? IsVoteDecending { get; set; }

}
