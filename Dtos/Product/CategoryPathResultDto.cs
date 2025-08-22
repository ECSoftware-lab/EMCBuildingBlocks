namespace EMC.BuildingBlocks.Dtos.Product
{
    public class CategoryPathResultDto
    {
        public List<CategoryPathDto>? Categories { get; set; } = new();
        public string? PathText { get; set; } //=> Categories != null && Categories.Count > 0 ? string.Join(" > ", Categories.Select(x => x.Name)) : null;
    }
   
   
   
}
