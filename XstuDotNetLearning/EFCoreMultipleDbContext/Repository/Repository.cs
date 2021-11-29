using EFCoreMultipleDbContext.EntityFrameworkCore;

namespace EFCoreMultipleDbContext.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly MainDbContext _mainDbContext;

        public Repository(MainDbContext dbContext)
        {
            _mainDbContext = dbContext;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await _mainDbContext.AddAsync(entity);
            await _mainDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
