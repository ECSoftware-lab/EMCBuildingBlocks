namespace EMC.BuildingBlocks.EventBus.IntegrationEvents
{
    public class ProductEventDto
    {
        public Guid ProductId { get; set; }
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }
        public string? CodBarra { get; set; }
        public bool LookAtSales { get; set; }
        public bool Integrity { get; set; }
        public bool ArticleHeader { get; set; }
        public List<string>? Images { get; set; }
        public List<AttributeEventDto>? ProductGroups { get; set; }
        public List<AttributeOtherEventDto>? OtherAttributes { get; set; }
        public List<CategoryEventDto>? Categories { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class AttributeEventDto
    {
        public int AttrId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
    }
    public class AttributeOtherEventDto
    {
        public int AttrId { get; set; }
        public string Name { get; set; }
        public List<string> Value { get; set; } = new();
        public int Order { get; set; }
        public bool IsRequired { get; set; }
    }
    public class CategoryEventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
