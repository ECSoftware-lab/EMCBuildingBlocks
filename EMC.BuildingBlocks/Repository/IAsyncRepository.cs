using EMC.BuildingBlocks.Application.Pagination;
using System.Linq.Expressions;

namespace EMC.BuildingBlocks.Repository
{
    public interface IAsyncRepository<T,TId> where T : class, IEntityWithId<TId>
    {
        Task<IReadOnlyList<T>> GetAllAsync(bool disableTracking = true);
        Task<PaginatedResult<T>> ToPaginatedResultAsync(IQueryable<T> query, BasePaginationRequest request, string observation = null, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true);
        Task<T> GetByIdAsync(TId id, bool disableTracking = true);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IQueryable<T>> GetAllFilt(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsyncCustom(T entity);

        Task<T> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate = null,
        List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true);

        /*  
Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
string includeString = null,
bool disableTracking = true);



Task<T> GetByIdAsync(int id, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true);


void DetachEntityIfTracked<TEntity>(TEntity entity) where TEntity : class;

*/
    }
}
