using StackExchange.Redis;
using System;

namespace Core.Cache;

using StackExchange.Redis;
using System;

public class RedisCache
{
    private readonly string _connectionString;
    private readonly string _instanceName;
    private readonly TimeSpan _defaultExpiration;

    public RedisCache(string connectionString, string instanceName, TimeSpan defaultExpiration)
    {
        _connectionString = connectionString;
        _instanceName = instanceName;
        _defaultExpiration = defaultExpiration;
    }

    private ConnectionMultiplexer GetConnection()
    {
        return ConnectionMultiplexer.Connect(_connectionString);
    }

    private string GetKey(string key)
    {
        return $"{_instanceName}:{key}";
    }

    public T Get<T>(string key)
    {
        var redis = GetConnection().GetDatabase();
        var cacheKey = GetKey(key);
        var value = redis.StringGet(cacheKey);

        if (!value.HasValue)
        {
            return default;
        }

        return Deserialize<T>(value);
    }

    public void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        var redis = GetConnection().GetDatabase();
        var cacheKey = GetKey(key);

        redis.StringSet(cacheKey, Serialize(value), expiration ?? _defaultExpiration);
    }

    public bool Remove(string key)
    {
        var redis = GetConnection().GetDatabase();
        var cacheKey = GetKey(key);

        return redis.KeyDelete(cacheKey);
    }

    private byte[] Serialize<T>(T value)
    {
        if (value == null)
        {
            return null;
        }

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        return System.Text.Encoding.UTF8.GetBytes(json);
    }

    private T Deserialize<T>(byte[] bytes)
    {
        if (bytes == null)
        {
            return default;
        }

        var json = System.Text.Encoding.UTF8.GetString(bytes);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    }
}