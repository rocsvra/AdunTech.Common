using Microsoft.AspNetCore.Mvc;

namespace AdunTech.IdcardOcr.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IBaiduIdcardOcrService _ocrService;
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger, IBaiduIdcardOcrService ocrService)
        {
            _logger = logger;
            _ocrService = ocrService;
        }

        [HttpPost("BackSide")]
        [ProducesResponseType(typeof(BackSide), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public ActionResult<BackSide> BackSide(IFormFile file)
        {
            byte[] image = new byte[file.Length];
            file.OpenReadStream().Read(image, 0, (int)file.Length);
            return _ocrService.GetBackInfo(image);
        }
    }
}