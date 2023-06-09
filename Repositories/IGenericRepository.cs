using System.Linq.Expressions;

namespace rtperson.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetListAsync();
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);

        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<TResult> GetAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);
        Task CreateAsync(T newEntity);
        Task UpdateAsync(T updatedEntity);
        Task DeleteAsync(Guid id);
    }
}
