using System;
using System.Linq.Expressions;

namespace Shop.API.Services;

public interface IBaseService<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<IQueryable<T>> GetAllAsync();
    Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
    Task<int> CountAsync();
    Task<bool> ExistAsync(Expression<Func<T, bool>> expression);
    Task SaveChangesAsync();
}
