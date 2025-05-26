namespace EMC.BuildingBlocks.Context
{
    public interface ICompanyExecutionContext
    {
        Guid UserId { get; }
        string UserName { get; }
        int CompanyId { get; }
        
        List<string> Roles { get; }
        Dictionary<string, string> Claims { get; }
        int? ActiveSubsidiaryId { get; }
        Dictionary<string, string> Configurations { get; set; }
    }

}
