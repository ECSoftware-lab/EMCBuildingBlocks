namespace EMC.BuildingBlocks.Context
{
    public class CompanyExecutionContext : ICompanyExecutionContext
    {
        public Guid UserId { get; set; }
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new();
        public Dictionary<string, string> Claims { get; set; } = new();
        public int? ActiveSubsidiaryId { get; set; }
        public Dictionary<string, string> Configurations { get; set; } = new();

    }
}
