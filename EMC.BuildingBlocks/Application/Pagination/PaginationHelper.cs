using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EMC.BuildingBlocks.Application.Pagination
{
    public static class PaginationHelper
    {
        public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
        IQueryable<T> query,
        BasePaginationRequest request,
        string observation = null,
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

            return new PaginatedResult<T>(items, totalItems, request.CurrentPage, request.PageSize, observation);
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
