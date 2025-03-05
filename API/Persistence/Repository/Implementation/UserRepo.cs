using API.Persistence.Context;
using API.Persistence.Entities;
using API.Persistence.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository.Implementation
{
    public class UserRepo : Repository<User>, IUserRepo
    {
        public UserRepo(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<User?> GetByUserName(string userName)
        {
            var queryable = GetQueryable();
            return await queryable.FirstOrDefaultAsync(x => x.Username == userName);
        }
    }
}