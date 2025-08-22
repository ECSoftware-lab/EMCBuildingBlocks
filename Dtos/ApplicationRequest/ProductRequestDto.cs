namespace EMC.BuildingBlocks.Dtos.ApplicationRequest
{
    public class ProductRequestDto
    {
        public int IdEmployed { get; set; }
        public Guid CompanyId { get; set; }
        public string? CadBarra { get; set; }
        public bool LookAtSales { get; set; }
        public bool? Integrity { get; set; }
        public bool ArticleHeader { get; set; }
        public string? Article { get; set; }
        public Dictionary<string, List<string>> AttributesShared { get; set; }
        public Dictionary<int, int> AttributesUnique { get; set; }
        public List<int> CompanyCategoryIds { get; set; } = new List<int>();
    }
}
