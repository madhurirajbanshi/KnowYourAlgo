using API.Persistence.Context;
using API.Persistence.Entities;
using API.Persistence.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository.Implementation;

public class TagRepo : Repository<Tag>, ITagRepo
{
    public TagRepo(ApplicationDbContext context) : base(context)
    {
    }
}
