﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdunTech.Redis.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IRedisService _redisService;
        private readonly RedisOptions _redisOptions;

        public TestController(IRedisService redisService
            , IOptions<RedisOptions> redisOptions)
        {
            _redisService = redisService;
            _redisOptions = redisOptions.Value;
        }

        /// <summary>
        /// 字符串存储
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            _redisService.Set("name", "Life for Aiur");
            return _redisService.Get("name");
        }

        /// <summary>
        /// 对象存储
        /// </summary>
        /// <returns></returns>
        [HttpGet("T")]
        public RedisOptions GetT()
        {
            _redisService.Set("t", _redisOptions);
            return _redisService.Get<RedisOptions>("t");
        }
    }
}
