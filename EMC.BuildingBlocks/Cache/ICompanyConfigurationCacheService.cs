namespace EMC.BuildingBlocks.Cache
{
    public interface ICompanyConfigurationCacheService
    {
        Task<Dictionary<string, string>?> GetCompanyConfigAsync(int companyId);
        Task RemoveCompanyConfigAsync(int companyId);
        Task SetCompanyConfigAsync(int companyId, Dictionary<string, string> config);
    }
}
