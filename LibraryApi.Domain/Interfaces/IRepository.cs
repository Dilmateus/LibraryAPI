using System.Linq.Expressions;

namespace LibraryApi.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>> include = null // Permite o uso de Includes
        );
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<TResult>> GetSelectedColumnsAsync<TResult>(Expression<Func<T, TResult>> selector);
    }
}
