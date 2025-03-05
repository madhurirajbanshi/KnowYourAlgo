using API.Dtos;
using API.Dtos.Algorithm;
using API.Dtos.Tag;
using API.Persistence.Entities;
using API.Persistence.UnitOfWork;
using API.Services.Interface;

namespace API.Services.Implementation;

public class AlgorithmService : IAlgorithmService
{
    private readonly IUnitOfWork _wof;
    public AlgorithmService(IUnitOfWork wof)
    {
        _wof = wof;
    }
    public async Task<ResultDto> CreateDto(AlgorithmCreateDto dto)
    {
      
        
        var algorithm = new Algorithm
        {
            Title = dto.Title,
        };
        try
        {
            await _wof.Algorithms.InsertAsync(algorithm);
            await _wof.SaveChangesAsync();
            return new ResultDto(true, "Algorithm created successfully");
        }
        catch (Exception e)
        {
            return new ResultDto(false, e.Message);

        }
    }

    public async Task<ResultWithDataDto<List<AlgorithmDto>>> GetAlgorithms()
    {
        var algorithms = await _wof.Algorithms.ListAsync();
        var dtos = algorithms.Select(x => new AlgorithmDto
        {
           Id = x.Id,
           Title = x.Title,
        }).ToList();
        return new ResultWithDataDto<List<AlgorithmDto>>(true, dtos, null);
    }

}
