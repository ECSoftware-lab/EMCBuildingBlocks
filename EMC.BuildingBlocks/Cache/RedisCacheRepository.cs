using Newtonsoft.Json;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace EMC.BuildingBlocks.Cache
{
    public class RedisCacheRepository : IRedisCacheRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _db;
        public RedisCacheRepository(IConnectionMultiplexer redis)
        {
            _redisConnection = redis;
            _db = redis.GetDatabase();
        }
        public async Task<T?> GetAsync<T>(string key)
        {

            var json = await _db.StringGetAsync(key);
            if (json.IsNullOrEmpty) return default;
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<List<T>?> GetList<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value.HasValue ? JsonConvert.DeserializeObject<List<T>>(value) : default;
        }

        public Task<bool> IsConnectedAsync()
        {
            return Task.FromResult(_redisConnection.IsConnected);
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonConvert.SerializeObject(value);
            await _db.StringSetAsync(key, json, expiry);
        }

        public async Task SetList<T>(string keyName, List<T> list, TimeSpan? expiry = null)
        {
            if (_redisConnection == null || !await IsConnectedAsync())
                return;
            var json = JsonConvert.SerializeObject(list);
            await _db.StringSetAsync(keyName, json, expiry);
        }


    }
}
