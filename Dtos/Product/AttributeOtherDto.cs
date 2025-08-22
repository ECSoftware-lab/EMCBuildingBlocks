namespace EMC.BuildingBlocks.Dtos.Product
{
    public class AttributeOtherDto
    {
        public int AttrId { get; set; } // CompanyAttributeId
        public string Name { get; set; }
        public List<string> Value { get; set; } = new();
        public int Order { get; set; }
        public bool IsRequired { get; set; }
    }
}
