﻿namespace TaskManagementSystem.Models.Interfaces;

public interface IBaseRepository<T> where T : class 
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(int id);

}
