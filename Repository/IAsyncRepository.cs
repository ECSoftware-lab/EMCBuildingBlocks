using EMC.BuildingBlocks.Application.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EMC.BuildingBlocks.Repository
{
    public interface IAsyncRepository<T,TId> where T : class, IEntityWithId<TId>
    {
        /// <summary>
        ///         
        /// </summary>
        /// <param name="id"></param>
        /// <param name="disableTracking"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(TId id, bool disableTracking = true, CancellationToken ct = default);
        Task<IReadOnlyList<T>> GetAllAsync(bool disableTracking = true);
        Task<PaginatedResult<T>> ToPaginatedResultAsync(IQueryable<T> query, BasePaginationRequest request, string observation = null, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true, CancellationToken ct = default);
        #region AddAsync
        Task<T> AddAsync(T entity, CancellationToken ct = default); 
        Task<T> AddAsync(DbContext context,T entity);
        #endregion
        #region update
        Task<bool> UpdateAsync(T entity);
        Task UpdateAsync(DbContext context, T entity);
        #endregion

        Task DeleteAsync(T entity);
        Task<List<T>> GetAllFilt(Expression<Func<T, bool>> filter = null);
     
        Task<T> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate = null,
        List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true, CancellationToken ct = default);
       
        Task DeleteAsync(T entity, DbContext context);
    }
}
