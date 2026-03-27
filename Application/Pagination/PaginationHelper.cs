using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EMC.BuildingBlocks.Application.Pagination
{
    public static class PaginationHelper
    {
        public static async Task<(List<Guid> ids, long total)> GetPagedIdsAsync(
     IQueryable<Guid> query,
     BasePaginationRequest filters,
     CancellationToken ct)
        {
            var total = await query.LongCountAsync(ct);

            var ids = await query
                .Skip((filters.CurrentPage - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .ToListAsync(ct);

            return (ids, total);
        }

        public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
        IQueryable<T> query,
        BasePaginationRequest request,
        List<string>? observations = null,
        CancellationToken cancellationToken = default)
        {
            if (!IsPropertyValid<T>.IsValid(request.Sort ?? "Id"))
                throw new Exception($"El campo de ordenación '{request.Sort}' no es válido.");

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, request.Sort ?? "Id");
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
            query = request.Order?.ToLower() == "desc" ? query.OrderByDescending(lambda) : query.OrderBy(lambda);

            var totalItems = await query.CountAsync(cancellationToken);
            var items = request.Paginate??true
                ? await query
                    .Skip((request.CurrentPage - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken)
                : await query.ToListAsync(cancellationToken);
            string observation = observations != null && observations.Count > 0 ? string.Join(";", observations) : "";
            return new PaginatedResult<T>(items, totalItems, request.CurrentPage, request.PageSize, observation);
        }
        public static async Task<PaginatedResult<int>> ToPaginatedIdsAsync<T>(
    IQueryable<T> query,
    BasePaginationRequest request, List<string>? observations = null,
    CancellationToken cancellationToken = default)
        {
            if (!IsPropertyValid<T>.IsValid(request.Sort ?? "Id"))
                throw new Exception($"El campo de ordenación '{request.Sort}' no es válido.");

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, request.Sort ?? "Id");
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
            query = request.Order?.ToLower() == "desc" ? query.OrderByDescending(lambda) : query.OrderBy(lambda);

            var totalItems = await query.CountAsync(cancellationToken);

            // Proyectamos solo el Id
            var idSelector = Expression.Lambda<Func<T, int>>(Expression.Property(parameter, "Id"), parameter);
            var idQuery = query.Select(idSelector);

            var ids = request.Paginate ?? true
                ? await idQuery
                    .Skip((request.CurrentPage - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken)
                : await idQuery.ToListAsync(cancellationToken);

            string observation = observations != null && observations.Count > 0 ? string.Join(";", observations) : "";
            return new PaginatedResult<int>(ids, totalItems, request.CurrentPage, request.PageSize, observation);
        }
        public static async Task<PaginatedResult<Guid>> ToPaginatedIdsGuidAsync<T>(
    IQueryable<T> query,
    BasePaginationRequest request,
    List<string>? observations = null,
    CancellationToken cancellationToken = default)
        {
            if (!IsPropertyValid<T>.IsValid(request.Sort ?? "Id"))
                throw new Exception($"El campo de ordenación '{request.Sort}' no es válido.");

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, request.Sort ?? "Id");
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
            query = request.Order?.ToLower() == "desc" ? query.OrderByDescending(lambda) : query.OrderBy(lambda);

            var totalItems = await query.CountAsync(cancellationToken);

            // Proyectamos solo el Id de tipo Guid
            var idSelector = Expression.Lambda<Func<T, Guid>>(Expression.Property(parameter, "Id"), parameter);
            var idQuery = query.Select(idSelector);

            var ids = request.Paginate ?? true
                ? await idQuery
                    .Skip((request.CurrentPage - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken)
                : await idQuery.ToListAsync(cancellationToken);
            string observation = observations != null && observations.Count > 0 ? string.Join(";", observations) : "";
            return new PaginatedResult<Guid>(ids, totalItems, request.CurrentPage, request.PageSize, observation);
        }

        public class IsPropertyValid<T>
        {
            private static readonly Dictionary<Type, HashSet<string>> _propertyCache = new();
            public static bool IsValid(string propName)
            {
                var type = typeof(T);
                if (!_propertyCache.TryGetValue(type, out var properties))
                {
                    properties = type.GetProperties().Select(p => p.Name).ToHashSet();
                    _propertyCache[type] = properties;
                }
                return properties.Contains(propName);
            }
        }
    }

}
