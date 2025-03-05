using API.Dtos;
using API.Dtos.Tag;

namespace API.Services.Interface;

public interface ITagService 
{
    Task<ResultWithDataDto<List<TagDto>>> GetTags();
}
