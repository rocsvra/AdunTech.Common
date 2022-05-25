using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdunTech.Redis.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IRedisService _redisService;
        private readonly ILogger<TestController> _logger;
        private readonly RedisOptions _redisOptions;

        public TestController(ILogger<TestController> logger
            , IRedisService redisService
            , IOptions<RedisOptions> redisOptions)
        {
            _logger = logger;
            _redisService = redisService;
            _redisOptions = redisOptions.Value;
        }

        [HttpGet]
        public string Get()
        {
            _redisService.Set("name", "Life for Aiur");
            return _redisService.Get("name");
        }

        [HttpGet("T")]
        public RedisOptions GetT()
        {
            _redisService.Set("t", _redisOptions);
            return _redisService.Get<RedisOptions>("t");
        }
    }
}
