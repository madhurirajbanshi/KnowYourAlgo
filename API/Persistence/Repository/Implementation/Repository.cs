using API.Persistence.Context;
using API.Persistence.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository.Implementation;

public class Repository<IEntity> : IRepository<IEntity> where IEntity : class
{

    protected readonly ApplicationDbContext _context;
    private readonly DbSet<IEntity> _dbSet;
   
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<IEntity>();
    }

    public int Count() => _dbSet.Count();
    

    public async Task<int> CountAsync() => await _dbSet.CountAsync();

    public void Delete(IEntity entity) => _dbSet.Remove(entity);

    public void DeleteRange(IEnumerable<IEntity> entities) => _dbSet.RemoveRange(entities);

    public void Dispose() => _context.Dispose();

    public IEntity? Find(int id) => _dbSet.Find(id);

    public async Task<IEntity?> FindAsync(int id) => await _dbSet.FindAsync(id);

    public IAsyncEnumerable<IEntity> GetAsyncEnumerable() => _dbSet.AsAsyncEnumerable();

    public DbSet<IEntity> GetEntitySet() => _dbSet;

    public IEnumerable<IEntity> GetEnumerable() => _dbSet.AsEnumerable();

    public IQueryable<IEntity> GetQueryable() => _dbSet.AsQueryable();

    public void Insert(IEntity entity) => _dbSet.Add(entity);

    public async Task InsertAsync(IEntity entity) => await _dbSet.AddAsync(entity);

    public void InsertRange(IEnumerable<IEntity> entities) => _dbSet.AddRange(entities);

    public async Task InsertRangeAsync(IEnumerable<IEntity> entities) => await _dbSet.AddRangeAsync(entities);

    public List<IEntity> List() => [.. _dbSet];

    public List<IEntity> List(int page, int pageSize) => [.. _dbSet.Skip(page * pageSize).Take(pageSize)];

    public async Task<List<IEntity>> ListAsync() => await _dbSet.ToListAsync();

    public async Task<List<IEntity>> ListAsync(int page, int pageSize) => await _dbSet.Skip(page * pageSize).Take(pageSize).ToListAsync();

    public void Update(IEntity entity) => _dbSet.Update(entity);
}
