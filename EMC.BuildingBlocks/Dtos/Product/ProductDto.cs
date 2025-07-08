using System.Text.Json.Serialization;

namespace EMC.BuildingBlocks.Dtos.Product;
public class ProductDto
{
    [JsonPropertyOrder(1)]
    public Guid Id { get; set; }
    [JsonPropertyOrder(2)]
    public Guid CompanyId { get; set; }
    [JsonPropertyOrder(3)]
    public bool Integrity { get; set; }

    [JsonPropertyOrder(4)]
    public List<AttributeDto> Attributes { get; set; }

    [JsonPropertyOrder(5)]
    public List<AttributeOtherDto>? OtherAttributes { get; set; }
    [JsonPropertyOrder(6)]
    public string CodBarra { get; set; }
    [JsonPropertyOrder(7)]
    public bool LookAtSales { get; set; }
    
    [JsonPropertyOrder(8)]
    public bool ArticleHeader { get; set; }

    [JsonPropertyOrder(9)]
    public CategoryPathResultDto? Categories { get; set; }
    [JsonPropertyOrder(10)]
    public BaseDto Base { get; set; }
    [JsonPropertyOrder(11)]
    public bool Status { get; set; }
}
