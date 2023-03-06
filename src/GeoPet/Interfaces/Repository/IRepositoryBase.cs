namespace GeoPet.Interfaces.Repository;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task<TEntity> Add(TEntity entity);
    Task<List<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task<TEntity> Update(int id, TEntity entity);
    Task<TEntity> GetById(Guid id);
    Task<TEntity> Update(Guid id, TEntity entity);
    Task<TEntity> Remove(TEntity entity);
}