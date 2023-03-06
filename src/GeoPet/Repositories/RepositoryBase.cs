using GeoPet.Interfaces.Repository;
using Microsoft.Data.SqlClient;
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
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException e)
        when (e.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
        {
            throw new Exception("Email já cadastrado");
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
            return await _dbSet.AsNoTracking().ToListAsync();
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
            var entity = await _dbSet.FindAsync(id);

            return entity!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar por id.");
        }
    }

    public async Task<TEntity> GetById(Guid id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);

            return entity!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar por id.");
        }
    }

    public async Task<TEntity> Update(int id, TEntity entity)
    {
        try
        {
            var entityToUpdate = await GetById(id);

            _dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            _dbContext.SaveChanges();

            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar atualizar.");
        }
    }

    public async Task<TEntity> Update(Guid id, TEntity entity)
    {
        try
        {
            var entityToUpdate = await GetById(id);

            _dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            _dbContext.SaveChanges();
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
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar deletar.");
        }
    }
}