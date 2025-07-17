using System.Text.Json;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class RedisCasheService(IDistributedCache distributedCache, ILogger<RedisCasheService> logger) : IRedisCasheService
{
    public async Task SetData<T>(string key, T data, int expirationMinutes)
    {
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationMinutes),
        };
        var jsonSerializerOption = new JsonSerializerOptions { WriteIndented = true };

        var jsonData = JsonSerializer.Serialize(data, jsonSerializerOption);
        await distributedCache.SetStringAsync(key, jsonData, options);
    }
    
    public async Task<T?> GetData<T>(string key)
    {
        var jsonData = await distributedCache.GetStringAsync(key);
        return jsonData != null
            ? JsonSerializer.Deserialize<T>(jsonData)
            : default;
    }

    public async Task DeleteData(string key)
    {
        await distributedCache.RemoveAsync(key);
    }



}
