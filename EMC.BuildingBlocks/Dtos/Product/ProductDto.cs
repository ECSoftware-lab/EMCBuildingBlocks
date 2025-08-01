
using Newtonsoft.Json;

namespace EMC.BuildingBlocks.Dtos.Product;
public class ProductDto
{
    [JsonProperty(Order = 1)]
    public Guid Id { get; set; }
    [JsonProperty(Order = 2)]
    public Guid CompanyId { get; set; }
    [JsonProperty(Order =3)]
    public bool Integrity { get; set; }

    [JsonProperty(Order = 4)]
    public List<AttributeDto> Attributes { get; set; }
    [JsonProperty(Order = 5)]
    public List<AttributeOtherDto>? OtherAttributes { get; set; }
    [JsonProperty(Order = 6)]
    public string CodBarra { get; set; }
    [JsonProperty(Order = 7)]
    public bool LookAtSales { get; set; }

    [JsonProperty(Order = 8)]
    public bool ArticleHeader { get; set; }

    [JsonProperty(Order = 9)]
    public CategoryPathResultDto? Categories { get; set; }
    [JsonProperty(Order = 10)]
    public BaseDto Base { get; set; }
    [JsonProperty(Order = 11)]
    public bool Status { get; set; }
}
