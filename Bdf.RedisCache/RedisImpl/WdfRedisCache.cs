using System;
using StackExchange.Redis;
using Bdf.RedisCache.Configuration;
using Bdf.RedisCache.Extensions;
using Bdf.Runtime.Caching;

namespace Bdf.RedisCache.RedisImpl
{
    public class BdfRedisCache : CacheBase
    {
         public const string ConnectionStringKey = "Abp.Redis.Cache";

        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public IDatabase Database
        {
            get
            {
                return _connectionMultiplexer.GetDatabase();
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public BdfRedisCache(string name, IBdfRedisConnectionProvider redisConnectionProvider)
            : base(name)
        {
            var connectionString = redisConnectionProvider.GetConnectionString(ConnectionStringKey);
            _connectionMultiplexer = redisConnectionProvider.GetConnection(connectionString);
        }
        public override object GetOrDefault(string key)
        {
            var objbyte = Database.StringGet(GetLocalizedKey(key));
            return objbyte.HasValue
                ? SerializeUtil.Deserialize(objbyte)
                : null;
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            if (value == null)
            {
                throw new BdfException("Can not insert null values to the cache!");
            }

            Database.StringSet(
                GetLocalizedKey(key),
                SerializeUtil.Serialize(value),
                slidingExpireTime
                );
        }

        public override void Remove(string key)
        {
            Database.KeyDelete(key);
        }

        public override void Clear()
        {
            Database.KeyDeleteWithPrefix(GetLocalizedKey("*"));
        }

        private string GetLocalizedKey(string key)
        {
            return "n:" + Name + ",c:" + key;
        }
    }
}