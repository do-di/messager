using Application.Interface.Service;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Service
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        // Add or update a connection ID for a user
        public async Task AddConnectionIdAsync(string userId, string connectionId)
        {
            var key = $"UserConnections:{userId}";
            var existingConnections = await GetConnectionIdsAsync(userId);

            if (!existingConnections.Contains(connectionId))
            {
                existingConnections.Add(connectionId);
                await _cache.SetStringAsync(key, JsonSerializer.Serialize(existingConnections));
            }
        }

        // Remove a connection ID for a user
        public async Task RemoveConnectionIdAsync(string userId, string connectionId)
        {
            var key = $"UserConnections:{userId}";
            var existingConnections = await GetConnectionIdsAsync(userId);

            if (existingConnections.Contains(connectionId))
            {
                existingConnections.Remove(connectionId);
                if (existingConnections.Count > 0)
                {
                    await _cache.SetStringAsync(key, JsonSerializer.Serialize(existingConnections));
                }
                else
                {
                    await _cache.RemoveAsync(key);
                }
            }
        }

        // Get all connection IDs for a user
        public async Task<List<string>> GetConnectionIdsAsync(string userId)
        {
            var key = $"UserConnections:{userId}";
            var cachedData = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedData))
            {
                return new List<string>();
            }

            var result = JsonSerializer.Deserialize<List<string>>(cachedData);
            if(result == null)
            {
                return new List<string>();
            }
            return result;
        }
    }
}
