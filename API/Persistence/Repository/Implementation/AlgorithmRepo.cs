using API.Persistence.Context;
using API.Persistence.Entities;
using API.Persistence.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository.Implementation;

public class AlgorithmRepo : Repository<Algorithm>, IAlgorithmRepo
{
    public AlgorithmRepo(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<Algorithm?> GetByTitle(string title)
    {
        var queryable = GetQueryable();
        return await queryable.FirstOrDefaultAsync(x => x.Title == title);
    }
}
