using EMC.BuildingBlocks.Dtos.Product;

namespace EMC.BuildingBlocks.Dtos.ApplicationRequest
{
    public interface IProductDto
    {
        Guid Id { get; set; }
        int IdEmployed { get; set; }
        Guid CompanyId { get; set; }
        bool? Integrity { get; set; }
        List<AttributeDto> Attributes { get; set; }//para los unicos 
        bool IsActive { get; set; }

    }
}
