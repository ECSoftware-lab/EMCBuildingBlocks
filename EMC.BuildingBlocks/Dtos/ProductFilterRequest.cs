using EMC.BuildingBlocks.Application.Pagination;

namespace EMC.BuildingBlocks.Dtos
{
    public record ProductFilterRequest : BasePaginationRequest
    {
        public string CompanyId { get; set; } 
        public bool? Integrity { get; set; }
        public string? CodBar { get; set; }
        public bool? ArticleHeader { get; set; }
        public bool? LookAtSales { get; set; }
        public int? NumFilter { get; set; }
        public string? TextFilter { get; set; }
        public int? StateFilter { get; set; }
        public string? StartDate { get; set; }
        public string? EndData { get; set; }
        public bool? CategoriInclud { get; set; }
        public bool? AtributInclud { get; set; }
        public bool? AtributOtherInclud { get; set; }
        public Dictionary<string, string> AttributeFilters { get; set; } = new();


    }
}
