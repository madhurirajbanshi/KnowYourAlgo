using API.Persistence.Entities;

namespace API.Persistence.Repository.Interface;

public interface IAlgorithmRepo : IRepository<Algorithm>
{
    Task<Algorithm> GetByTitle(string name);
}
