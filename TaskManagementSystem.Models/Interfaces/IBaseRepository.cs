using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TaskManagementSystem.Models.Interfaces;

public interface IBaseRepository<T> where T : class 
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(int id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);


}
