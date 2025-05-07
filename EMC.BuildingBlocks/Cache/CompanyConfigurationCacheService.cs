namespace EMC.BuildingBlocks.Cache
{
    public class CompanyConfigurationCacheService : ICompanyConfigurationCacheService
    {
        private readonly IRedisCacheRepository _cache;
        private const string KeyPrefix = "company:config:";

        public CompanyConfigurationCacheService(IRedisCacheRepository cache)
        {
            _cache = cache;
        }

        public Task SetCompanyConfigAsync(int companyId, Dictionary<string, string> config)
        {
            var key = $"{KeyPrefix}{companyId}";
            return _cache.SetAsync(key, config);
        }
        public Task RemoveCompanyConfigAsync(int companyId)
        {
            var key = $"{KeyPrefix}{companyId}";
            return _cache.RemoveAsync(key);
        }
        public Task<Dictionary<string, string>?> GetCompanyConfigAsync(int companyId)
        {
            var key = $"{KeyPrefix}{companyId}";
            return _cache.GetAsync<Dictionary<string, string>>(key);
        }


    }

}
