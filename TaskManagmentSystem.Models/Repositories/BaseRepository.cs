using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Interfaces;

namespace TaskManagmentSystem.Business.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;
    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }


    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _dbSet.FindAsync(id);
        if(result != null)
        {
            _dbSet.Remove(result);
        }
    }


    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }


    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await Task.FromResult(_dbSet.Where(predicate).ToList());
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.Where(predicate);
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await Task.FromResult(query);
    }



    public void Update(T entity)
    {
        _dbSet.Update(entity);  
    }
    
}
