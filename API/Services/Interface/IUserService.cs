using API.Persistence.Entities;
using API.Persistence.Repository.Interface;

namespace API.Services.Interface;

public interface IUserService
{
    Task<List<User>?> GetByUserName(string userName);
}
