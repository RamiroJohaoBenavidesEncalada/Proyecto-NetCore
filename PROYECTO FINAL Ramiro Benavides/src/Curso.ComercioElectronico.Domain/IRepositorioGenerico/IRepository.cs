
using System.Linq.Expressions;

namespace Curso.ComercioElectronico.Domain;


public interface IRepository<TEntity,TEntityId> where TEntity : class
{
    IUnitOfWork UnitOfWork { get; }
    
    IQueryable<TEntity> GetAll(bool asNoTracking = true);
    //TODO: utilizar genericos en el tipo de parametro id
    
    //Task<TEntity> GetByIdAsync(int id); Usando Genericos
    Task<TEntity> GetByIdAsync(TEntityId id);

    Task<TEntity> AddAsync(TEntity entity);

    Task UpdateAsync (TEntity entity);

    void  Delete(TEntity entity);

    IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

    Task<ICollection<TEntity>> GetAsync();
}
