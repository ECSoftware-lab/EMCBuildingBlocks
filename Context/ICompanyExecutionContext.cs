namespace EMC.BuildingBlocks.Context
{
    public interface ICompanyExecutionContext
    {
        Guid UserId { get; }
        string UserName { get; }
        Guid CompanyId { get; }
        string CompanyName { get; }
        string TimeZone { get; }
        List<string> Roles { get; }
        Dictionary<string, string> Claims { get; }
        int? ActiveSubsidiaryId { get; }
        Dictionary<string, string> Configurations { get; set; }
        int KindId { get; }
        bool CompanyEnabled { get; }
    }

}
