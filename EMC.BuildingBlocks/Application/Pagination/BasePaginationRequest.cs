namespace EMC.BuildingBlocks.Application.Pagination
{
    public record BasePaginationRequest
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; } = "Id"; // Default sort
        public string? Order { get; set; } = "asc";
        public bool? Paginate { get; set; } = true; 
        public int? NumFilter { get; init; }
    }
}
