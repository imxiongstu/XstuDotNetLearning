namespace EFCoreMultipleDbContext.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> FindAsync(TKey key);
    }
}
