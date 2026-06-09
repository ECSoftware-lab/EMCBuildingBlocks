namespace EMC.BuildingBlocks.Context
{
    public class CompanyExecutionContext : ICompanyExecutionContext
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; } = new();
        public Dictionary<string, string> Claims { get; set; } = new();
        public int? ActiveSubsidiaryId { get; set; }
        public Dictionary<string, string> Configurations { get; set; } = new();

        public int KindId { get; set; }

        public string CompanyName { get; set; }

        public string TimeZone =>
         Configurations.TryGetValue("timeZone", out var value) && !string.IsNullOrWhiteSpace(value)
             ? value
             : "UTC";

        public bool CompanyEnabled =>
       Configurations.TryGetValue("companyEnabled", out var value) &&
       string.Equals(value, "TRUE", StringComparison.OrdinalIgnoreCase);
    }
}
