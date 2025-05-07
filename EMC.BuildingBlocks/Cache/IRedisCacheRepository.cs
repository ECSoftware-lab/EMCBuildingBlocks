namespace EMC.BuildingBlocks.Cache
{
    public interface IRedisCacheRepository
    {
        Task<bool> IsConnectedAsync();

        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);

        Task RemoveAsync(string key);

        Task<List<T>?> GetList<T>(string key);
        Task SetList<T>(string keyName, List<T> list, TimeSpan? expiry = null);
    }

}
