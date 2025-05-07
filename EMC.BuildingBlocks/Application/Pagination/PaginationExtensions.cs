namespace EMC.BuildingBlocks.Application.Pagination
{
    public static class PaginationExtensions
    {
        //public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
        //    this IQueryable<T> query,
        //    int page,
        //    int pageSize,
        //    CancellationToken cancellationToken = default)
        //{
        //    var totalItems = await query.CountAsync(cancellationToken);
        //    var items = await query.Skip((page - 1) * pageSize)
        //                           .Take(pageSize)
        //                           .ToListAsync(cancellationToken);

        //    return new PaginatedResult<T>(items, totalItems, page, pageSize);
        //}
    }

}
