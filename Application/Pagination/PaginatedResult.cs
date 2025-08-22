namespace EMC.BuildingBlocks.Application.Pagination
{
    public class PaginatedResult<T>
    {
        public int CurrentPage { get; init; }
        public long TotalItems { get; init; }
        public int PageSize { get; init; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public int start => CurrentPage - 5;
        public int end => CurrentPage + 5;

        public int StartPage => start <= 0 ? 1 : Math.Min(start, TotalPages);
        public int EndPage => end > TotalPages ? TotalPages : Math.Min(end, TotalPages);
        public string Observation {  get; init; }
        public IReadOnlyList<T> Items { get; init; }

        public PaginatedResult(IReadOnlyList<T> items, long totalItems, int currentPage, int pageSize,string observation)
        {
            Items = items;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Observation=observation;
        }
    }

}
