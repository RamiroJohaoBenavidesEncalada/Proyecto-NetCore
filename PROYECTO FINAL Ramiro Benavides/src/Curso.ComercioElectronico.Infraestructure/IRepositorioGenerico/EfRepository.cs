
using Microsoft.EntityFrameworkCore;
using Curso.ComercioElectronico.Domain;
using System.Linq.Expressions;

namespace Curso.ComercioElectronico.Infraestructure;
 

public abstract class EfRepository<TEntity,TEntityId> : IRepository<TEntity,TEntityId> where TEntity : class 
{
    protected readonly ComercioElectronicoDbContext _context;

    public IUnitOfWork UnitOfWork => _context;


    public EfRepository(ComercioElectronicoDbContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> GetByIdAsync(TEntityId id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

   
    public virtual IQueryable<TEntity> GetAll(bool asNoTracking = true)
    {
        if (asNoTracking)
            return _context.Set<TEntity>().AsNoTracking();
        else
            return _context.Set<TEntity>().AsQueryable();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {

        await _context.Set<TEntity>().AddAsync(entity);

        return entity;
    }

    public virtual async  Task UpdateAsync(TEntity entity)
    {
          _context.Update(entity);
        
        return;
    }

    public virtual void  Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        
 
    }
 
    public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> queryable = GetAll();
        foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
        {
            queryable = queryable.Include<TEntity, object>(includeProperty);
        }

        return queryable;
    }

    public async Task<ICollection<TEntity>> GetAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

}