using API.Dtos;
using API.Dtos.Tag;
using API.Persistence.UnitOfWork;
using API.Services.Interface;

namespace API.Services.Implementation;

public class TagService : ITagService
{
    private readonly IUnitOfWork _uow;

    public TagService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ResultWithDataDto<List<TagDto>>> GetTags()
    {
        var tags = await _uow.Tags.ListAsync();
        var dtos = tags.Select(tag => new TagDto
        {
            Id = tag.Id,
            Name = tag.Name,
        }).ToList();
        return new ResultWithDataDto<List<TagDto>>(true, dtos, null);
    }
}
