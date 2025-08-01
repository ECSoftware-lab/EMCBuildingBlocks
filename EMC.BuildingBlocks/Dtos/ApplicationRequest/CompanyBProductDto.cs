using EMC.BuildingBlocks.Dtos.Product;

namespace EMC.BuildingBlocks.Dtos.ApplicationRequest
{
    public class CompanyBProductDto : IProductDto
    {

        public Guid Id { get; set; }
        public int IdEmployed { get; set; }
        public Guid CompanyId { get; set; }
        public bool? Integrity { get; set; } 
        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string Sku { get; set; }
        public List<AttributeDto> Attributes { get; set; }
    }
}
