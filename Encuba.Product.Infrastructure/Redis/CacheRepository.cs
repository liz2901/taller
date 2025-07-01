using System.Text.Json;
using Encuba.Product.Domain.Interfaces.Services;
using StackExchange.Redis;

namespace Encuba.Product.Infrastructure.Redis;

public class CacheRepository(RedisConfiguration redisConfiguration) : ICacheRepository
{
    private readonly IDatabase _db = redisConfiguration.Connection.GetDatabase();

    public async Task AddItemAsync<T>(string key, T value)
    {
        var jsonData = JsonSerializer.Serialize(value);
        var expiry = TimeSpan.FromMinutes(60);
        await _db.StringSetAsync(key, jsonData, expiry);
    }

    public async Task<T?> GetItemAsync<T>(string key)
    {
        var jsonData = await _db.StringGetAsync(key);

        if (string.IsNullOrEmpty(jsonData))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(jsonData);
    }

    public async Task DeleteItemAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }

    public async Task<IEnumerable<string>> ListAllKeysAsync()
    {
        var server = _db.Multiplexer.GetServer(_db.Multiplexer.GetEndPoints().First());
        return server.Keys().Select(k => k.ToString());
    }
}