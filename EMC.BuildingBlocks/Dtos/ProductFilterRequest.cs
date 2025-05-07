using EMC.BuildingBlocks.Application.Pagination;

namespace EMC.BuildingBlocks.Dtos
{
    public record ProductFilterRequest : BasePaginationRequest
    {
        public int CompanyId { get; set; }
        public int? BrandId { get; set; }
        public string? Article { get; set; }
        public int? NumFilter { get; set; }
        public string? TextFilter { get; set; }
        public int? StateFilter { get; set; }
        public string? StartDate { get; set; }
        public string? EndData { get; set; }
        public bool? CategoriInclud { get; set; }
        public bool? AtributInclud { get; set; }
        public bool? AtributOtherInclud { get; set; }
        public string? CodBar { get; set; }
    }
}
