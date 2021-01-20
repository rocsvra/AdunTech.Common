using NewLife.Caching;
using System;

namespace AdunTech.Redis
{
    public class RedisCacheClient
    {
        private readonly string redisConnenctionString;
        public volatile FullRedis redisConnection;
        private readonly object redisConnectionLock = new object();
        private readonly int db;

        public RedisCacheClient()
        {
            string redisConfiguration = Appsettings.App(new string[] { "AppSettings", "RedisCaching", "ConnectionString" });//获取连接字符串
            string Db = Appsettings.App(new string[] { "AppSettings", "RedisCaching", "Db" });
            if (string.IsNullOrWhiteSpace(redisConfiguration))
            {
                throw new ArgumentException("redis config is empty", nameof(redisConfiguration));
            }
            FullRedis.Register();
            this.redisConnenctionString = redisConfiguration;
            this.db = Db.ToInt();
            this.redisConnection = GetRedisConnection();
        }

        public RedisCacheClient(string redisConfiguration, int Db = 0)
        {
            if (string.IsNullOrWhiteSpace(redisConfiguration))
            {
                throw new ArgumentException("redis config is empty", nameof(redisConfiguration));
            }
            FullRedis.Register();
            this.redisConnenctionString = redisConfiguration;
            this.db = Db.ToInt();
            this.redisConnection = GetRedisConnection();
        }

        /// <summary>
        /// 核心代码，获取连接实例
        /// 通过双if 夹lock的方式，实现单例模式
        /// </summary>
        /// <returns></returns>
        private FullRedis GetRedisConnection()
        {
            //如果已经连接实例，直接返回
            if (this.redisConnection != null)
            {
                return this.redisConnection;
            }
            //加锁，防止异步编程中，出现单例无效的问题
            lock (redisConnectionLock)
            {
                if (this.redisConnection != null)
                {
                    //释放redis连接
                    this.redisConnection.Dispose();
                }
                try
                {
                    //this.redisConnection = FullRedis.Create(redisConnenctionString);
                    this.redisConnection = FullRedis.Create(redisConnenctionString);
                    this.redisConnection.Timeout = 5000;
                }
                catch (Exception ex)
                {

                    throw new Exception("Redis服务未启用，请开启该服务");
                }
            }
            return this.redisConnection;
        }

    }
}
