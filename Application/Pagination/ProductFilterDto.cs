namespace EMC.BuildingBlocks.Application.Pagination
{
    public record ProductFilterDto : BasePaginationRequest
    {
        public Guid CompanyId { get; init; }
        public Guid? Id { get; set; }
        public bool? Status { get; init; }
        public List<AttributesForFilterDto> AttributesGrup { get; set; }

        public Dictionary<string, string>? Filters { get; set; } = new();

        public List<string>? CategoryNames { get; set; } = new List<string>();
        public Dictionary<string, List<string>>? AttributeShareds { get; set; } = new Dictionary<string, List<string>>();


        //propios del filtro 
        public int FilterType { get; init; }
        public string? TextFilter { get; init; }

        //para respuesta no los mapea no necesito esto aqui  CategoriInclud AttributesGrupInclud AtributSharedInclud


        // Paginación y orden ya estan en        BasePaginationRequest

    }
    public record AttributesForFilterDto
    {
        public int TypeId { get; set; }
        public string Value { get; set; }
    }
}
