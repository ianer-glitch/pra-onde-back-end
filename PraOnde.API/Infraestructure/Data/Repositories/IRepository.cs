using System.Linq.Expressions;

namespace PraOnde.API.Infraestructure.Data.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    Task<T> InsertAsync(T entity);
    T Update(T entity);
    Task<int> SaveChangesAsync();
    Task AddRangeAsync(IEnumerable<T> entities);
    void UpdateRange(IEnumerable<T> entities);
    Task AddAsync(T entity);
}