namespace GeoPet.Interfaces.Repository;

public interface IRepositoryBase<TEntity> where TEntity: class
{
    Task<TEntity> Add(TEntity entity);
    Task<List<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task<TEntity> Update(TEntity entity);
    Task<TEntity> Remove(TEntity entity);
}