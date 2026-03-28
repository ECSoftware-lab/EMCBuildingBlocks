using EMC.BuildingBlocks.Application.Pagination;

namespace EMC.BuildingBlocks.Dtos.Person
{
    public record PersonFilterDto : BasePaginationRequest
    {
        public string CompanyId { get; set; }
        public Dictionary<string, string> AttributeFilters { get; set; } = new();
     
        public bool? IsUnique { get; set; }

        public List<string>? PersonType { get; set; }
        public int PersonTypeEfect { get; set; }
        public DateTime? CreateIn { get; set; }
        public DateTime? CreateEnd { get; set; }
    }
}
