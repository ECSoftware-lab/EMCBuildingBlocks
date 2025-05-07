namespace EMC.BuildingBlocks.Application.Pagination
{
    public static class QueryableHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, BasePaginationRequest request)
        {
            return query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);
        }

    }
}
