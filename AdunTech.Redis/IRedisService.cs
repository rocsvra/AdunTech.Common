using System;
using System.Collections.Generic;

namespace AdunTech.Redis
{
    public interface IRedisService
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取字符串对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 批量获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        IDictionary<string, T> GetAll<T>(IEnumerable<string> keys);

        /// <summary>
        /// 设置字符串对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间（秒）</param>
        /// <returns>返回是否执行成功</returns>
        bool Set(string key, string value, int expire = -1);

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间（秒）</param>
        /// <returns>返回是否执行成功</returns>
        bool Set<T>(string key, T value, int expire = -1);

        /// <summary>
        /// 批量设置对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="expire"></param>
        void SetAll<T>(IDictionary<string, T> values, int expire = -1);

        /// <summary>
        /// 设置键过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        bool SetExpire(string key, TimeSpan expire);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int Remove(string key);

        /// <summary>
        /// 批量移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int RemoveAll(IEnumerable<string> keys);
    }
}
