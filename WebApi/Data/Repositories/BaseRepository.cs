using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using WebApi.Data.Contexts;

namespace WebApi.Data.Repositories;

public class BaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context; //Dependency Injection with CONSTRUCTOR for '_context'
    protected readonly DbSet<TEntity> _table;

    protected BaseRepository(DataContext context) //CONSTRUCTOR FOR '_context' and '_dbSet'
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    //CRUD FÖR ALLA REPOSITORY SKRIVS ENDAST HÄR! DONT REPEAT YOUR SELF
    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);

            _table.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(expression);

            var entity = await _table.FirstOrDefaultAsync(expression);
            if (entity == null)
                return false;

            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await _table.ToListAsync();
        return entities;
    }

    //entity => entity.ClientName == "NOME ESPECÍFICO"
    //entity => entity.ClientiD == 1
    //entity => entity.Email == "luiz@oliveira" OK
    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _table.FirstOrDefaultAsync(expression);
        return entity;
    }

}
