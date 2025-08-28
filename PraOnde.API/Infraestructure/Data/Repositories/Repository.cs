using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PraOnde.API.Infraestructure.Data.Repositories;

public class Repository<T> where T: class , IRepository<T>
{
    private readonly Context _context;
    public Repository(Context context)
    {
        _context = context;
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);  
    }
    
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(expression);  
    }
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().AnyAsync(expression);  
    }

    public async Task<T> InsertAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public T Update(T entity)
    {
        _context.Update(entity);
        return entity;
        
    }

    public async Task<int> SaveChangesAsync()
    {
        return  await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
    }
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
    }
}