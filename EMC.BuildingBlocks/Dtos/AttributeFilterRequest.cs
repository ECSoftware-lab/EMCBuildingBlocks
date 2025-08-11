using EMC.BuildingBlocks.Application.Pagination;

namespace EMC.BuildingBlocks.Dtos
{
    public record AttributeFilterRequest : BasePaginationRequest
    {
        public string CompanyId { get; set; }
        public int? CompanyAttributeId { get; set; }


    }
}
