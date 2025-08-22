using EMC.BuildingBlocks.Dtos.Product; 

namespace EMC.BuildingBlocks.Dtos.ApplicationRequest
{
    public class CompanyAProductDto : IProductDto
    {
        public Guid Id { get; set; }
        public int IdEmployed { get; set; }
        public Guid CompanyId { get; set; }
        public bool? Integrity { get; set; }
       
        public bool IsActive { get; set; }

        public string? CadBarra { get; set; }
        public bool? LookAtSales { get; set; }
        public bool? ArticleHeader { get; set; }
        public List<AttributeDto> Attributes { get; set; } 
        public List<AttributeOtherDto>? OtherAttributes { get; set; } 
        public CategoryPathResultDto? Categories { get; set; }
        public int? SharedAttributeSetId { get; set; }

    }
}
