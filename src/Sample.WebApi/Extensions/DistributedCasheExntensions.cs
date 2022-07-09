using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Sample.WebApi.Extensions
{
    public static class DistributedCasheExtensions
    {
        public static async Task SetRecordAsync<T>(
            this IDistributedCache cashe,
            string recordId,
            T recordValue,
            TimeSpan? expireTime = null,
            TimeSpan? unusedTime = null)
        {
            DistributedCacheEntryOptions options = new()
            {
                AbsoluteExpirationRelativeToNow = expireTime ?? TimeSpan.FromSeconds(30),
                SlidingExpiration = unusedTime
            };

            var json = JsonSerializer.Serialize(recordValue);
            await cashe.SetStringAsync(recordId, json, options);
        }

        public static async Task<T> GetRecordAsync<T>(
            this IDistributedCache cashe,
            string recordId)
        {
            var json = await cashe.GetStringAsync(recordId);

            if (json == null) return default(T);

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
