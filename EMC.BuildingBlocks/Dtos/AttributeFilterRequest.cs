using EMC.BuildingBlocks.Application.Pagination;

namespace EMC.BuildingBlocks.Dtos
{
    public record AttributeFilterRequest : BasePaginationRequest
    {
        public string CompanyId { get; set; } 
        public int? CompanyAttributeId { get; set; }//me da el type del atributo para la company ? 
        public string? TextFilter { get; set; }// si escribo "RO" devuelve los "ROJO" "ROSAS" o lo que contenta ro en value
        public bool? Status { get; set; } //los estados IsActive 
     
    }
}
