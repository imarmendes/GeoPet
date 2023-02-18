using GeoPet.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GeoPet.Repository;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }
    
    public async Task<TEntity> Add(TEntity entity)
    {
        try
        {
            _dbSet.Add(entity);
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar criar.");
        }
    }

   public async Task<List<TEntity>> GetAll()
    {
        try
        {
            return _dbSet.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar todos.");
        }
    }
   
   public async Task<TEntity> GetById(int id)
   {
       try
       {
           return _dbSet.Find(id);
       }
       catch (Exception e)
       {
           Console.WriteLine(e);
           throw new Exception("Erro ao tentar buscar por id.");
       }
   }

    public async Task<TEntity> Update(TEntity entity)
    {
        try
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar atualizar.");
        }
    }

    public async Task<TEntity> Remove(TEntity entity)
    {
        try
        {
            _dbSet.Remove(entity);
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar deletar.");
        }
    }
}