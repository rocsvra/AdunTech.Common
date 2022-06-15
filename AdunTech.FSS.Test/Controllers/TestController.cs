using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace AdunTech.FSS.Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IFssService _fssService;
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger, IFssService fssService)
        {
            _logger = logger;
            _fssService = fssService;
        }

        [HttpPost("Upload/Single")]
        [DisableRequestSizeLimit]
        public ActionResult<string> Post(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            using (Stream fileStream = file.OpenReadStream())
            {
                var str = _fssService.UploadFile(fileStream, extension);
                return Ok(str);
            }
        }

        [HttpPost("Upload")]
        [DisableRequestSizeLimit]
        public ActionResult<List<string>> Post(IFormFileCollection files)
        {
            if (files.Count == 0)
            {
                return BadRequest("没有文件上传");
            }
            if (files.Sum(o => o.Length) > 10 * 1024 * 1024 * files.Count)
            {
                return BadRequest("文件过大，单个文件不能大于10M");
            }
            List<string> fileUrls = new List<string>();
            foreach (var file in files)
            {
                string extension = Path.GetExtension(file.FileName);
                using (Stream fileStream = file.OpenReadStream())
                {
                    string filePath = _fssService.UploadFile(fileStream, extension);
                    fileUrls.Add(filePath);
                }
            }
            return fileUrls;
        }

        [HttpGet("Download/{fileName}")]
        public ActionResult Get(string fileName)
        {
            fileName = HttpUtility.UrlDecode(fileName);
            byte[] response = _fssService.DownloadFile(fileName);
            MemoryStream stream = new MemoryStream(response, true);
            return File(stream, "application/octet-stream", fileName);
        }

        [HttpGet("Remove/{fileName}")]
        public ActionResult Remove(string fileName)
        {
            fileName = HttpUtility.UrlDecode(fileName);
            _fssService.RemoveFile(fileName);
            return Ok();
        }
    }
}