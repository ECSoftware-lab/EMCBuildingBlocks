namespace EMC.BuildingBlocks.Cache
{
    public interface ICompanyConfigurationCacheService
    {
        Task<Dictionary<string, string>?> GetCompanyConfigAsync(Guid companyId);
        Task<Dictionary<int, bool>?> GetCompanyConfigPersonTypeAsync(Guid companyId);
        Task RemoveCompanyConfigAsync(Guid companyId);
        Task RemoveCompanyConfigPersonTypeAsync(Guid companyId);
        Task SetCompanyConfigAsync(Guid companyId, Dictionary<string, string> config);
        Task SetCompanyConfigPersonTypeAsync(Guid companyId, Dictionary<int, bool> config);
    }
}
