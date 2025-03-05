using API.Dtos;
using API.Dtos.Algorithm;
using API.Dtos.Visualizations;

namespace API.Services.Interface
{
    public interface IVisualizationService
    {
        Task<ResultWithDataDto<List<VisualizationDto>>> GetVisualizations(VisualizationFilters filters, int userId=0);
        Task<ResultWithDataDto<VisualizationDto>> GetVisualization(int id, int userId=0);
        Task<ResultDto> CreateVisualization(VisualizationCreateDto dto, int userId);
        Task<ResultDto> LikeVisualization(int algoId, int userId);

    }
}
