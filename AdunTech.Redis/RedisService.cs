using Microsoft.Extensions.Options;
using NewLife.Caching;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdunTech.Redis
{
    public class RedisService : IRedisService
    {
        private readonly FullRedis _redis;

        public RedisService(RedisOptions options)
        {
            _redis = new FullRedis(options.ServerHost, options.Password, options.Db);
        }

        public RedisService(IOptions<RedisOptions> options)
        {
            _redis = new FullRedis(options.Value.ServerHost, options.Value.Password, options.Value.Db);
        }

        /// <summary>
        /// 连接串
        /// </summary>
        /// <param name="connectString"></param>
        public RedisService(string connectString)
        {
            _redis = new FullRedis();
            _redis.Init(connectString);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return _redis.ContainsKey(key);
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public void Clear()
        {
            _redis.Clear();
        }

        /// <summary>
        /// 获取字符串对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return _redis.Get<string>(key);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return _redis.Get<T>(key);
        }

        /// <summary>
        /// 批量获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            return _redis.GetAll<T>(keys);
        }

        /// <summary>
        /// 设置字符串对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间（秒）</param>
        /// <returns>返回是否执行成功</returns>
        public bool Set(string key, string value, int expire = -1)
        {
            return _redis.Set(key, value, expire);
        }

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间（秒）</param>
        /// <returns>返回是否执行成功</returns>
        public bool Set<T>(string key, T value, int expire = -1)
        {
            return _redis.Set(key, value, expire);
        }

        /// <summary>
        /// 批量设置对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="expire"></param>
        public void SetAll<T>(IDictionary<string, T> values, int expire = -1)
        {
            _redis.SetAll(values, expire);
        }

        /// <summary>
        /// 设置键过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public bool SetExpire(string key, TimeSpan expire)
        {
            return _redis.SetExpire(key, expire);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Remove(string key)
        {
            return _redis.Remove(key);
        }

        /// <summary>
        /// 批量移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int RemoveAll(IEnumerable<string> keys)
        {
            return _redis.Remove(keys.ToArray());
        }
    }
}
