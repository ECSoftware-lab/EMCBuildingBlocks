using EMC.BuildingBlocks.Application.Pagination;
using System.Linq.Expressions;

namespace EMC.BuildingBlocks.Repository
{
    public interface IAsyncRepository<T> where T : BaseDomain
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        //IQueryable<TModelo> Ordering<TModelo>(BasePaginationRequest request, IQueryable<TModelo> query, bool pagination = false) where TModelo : BasePaginationRequest;
        Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(IQueryable<T> query, BasePaginationRequest request, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       string includeString = null,
                                       bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       List<Expression<Func<T, object>>> includes = null,
                                       bool disableTracking = true);


        Task<T> GetByIdAsync(Guid id, bool disableTracking = true);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true);
        Task<IQueryable<T>> GetAllFilt(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsyncCustom(T entity);
        void DetachEntityIfTracked<TEntity>(TEntity entity) where TEntity : class;
    }
}
