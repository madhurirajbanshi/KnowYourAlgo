
using API.Persistence.Context;
using API.Persistence.Entities;
using API.Persistence.Repository.Interface;

namespace API.Persistence.Repository.Implementation;

public class VisualizationRepo : Repository<Visualization>, IVisualizationRepo
{
    public VisualizationRepo(ApplicationDbContext context) : base(context)
    {
    }
}
