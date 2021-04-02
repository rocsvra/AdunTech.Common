using AdunTech.ExceptionDetail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdunTech.HttpException.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get(int type)
        {
            switch (type)
            {
                case 1:
                    throw new BadRequestException(HttpContext.TraceIdentifier, "10086", Guid.NewGuid().ToString(),"fdsafdas");
                case 2:
                    throw new InternalServerException(HttpContext.TraceIdentifier, new InvalidOperationException("序列包含一个以上的元素"));
                case 3:
                    throw new Exception("10086");
                case 4:
                    return BadRequest("3333");
                case 5:
                    throw new ResourceNotFoundException(HttpContext.TraceIdentifier, "10086", Guid.NewGuid().ToString());
            }

            return Ok();
        }
    }
}
