namespace EMC.BuildingBlocks.Context
{
    public interface ICompanyExecutionContext
    {
        Guid UserId { get; }
        int CompanyId { get; }
        string Email { get; }
        List<string> Roles { get; }
        Dictionary<string, string> Claims { get; }
        int? ActiveSubsidiaryId { get; }
        Dictionary<string, string> Configurations { get; set; }
    }

}
