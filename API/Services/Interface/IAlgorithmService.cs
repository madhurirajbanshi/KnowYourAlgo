using API.Dtos;
using API.Dtos.Algorithm;

namespace API.Services.Interface;

public interface IAlgorithmService
{
    Task<ResultDto> CreateDto(AlgorithmCreateDto algorithmCreateDto);
    Task<ResultWithDataDto<List<AlgorithmDto>>> GetAlgorithms();
}
