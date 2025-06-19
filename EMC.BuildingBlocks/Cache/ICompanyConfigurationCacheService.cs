namespace EMC.BuildingBlocks.Cache
{
    public interface ICompanyConfigurationCacheService
    {
        Task<Dictionary<string, string>?> GetCompanyConfigAsync(Guid companyId);
        Task RemoveCompanyConfigAsync(Guid companyId);
        Task SetCompanyConfigAsync(Guid companyId, Dictionary<string, string> config);
    }
}
