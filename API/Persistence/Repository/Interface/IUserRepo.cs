using API.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository.Interface;

public interface IUserRepo : IRepository<User>
{
   Task<User?> GetByUserName(string userName);
}