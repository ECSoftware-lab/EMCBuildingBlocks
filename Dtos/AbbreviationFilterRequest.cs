using EMC.BuildingBlocks.Application.Pagination;

namespace EMC.BuildingBlocks.Dtos
{
    public record AbbreviationFilterRequest : BasePaginationRequest
    {
        public string CompanyId { get; set; }
        public int? AbbreviationId { get; set; }
        public int AbbreviationType { get; set; }


    }
}
