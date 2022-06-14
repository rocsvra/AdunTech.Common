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

        [HttpPost]
        [DisableRequestSizeLimit]
        public ActionResult<List<string>> Post(IFormFileCollection files)
        {
            if (files.Count == 0)
            {
                return BadRequest("û���ļ��ϴ�");
            }
            if (files.Sum(o => o.Length) > 10 * 1024 * 1024 * files.Count)
            {
                return BadRequest("�ļ����󣬵����ļ����ܴ���10M");
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

        [HttpGet("Download/{id}")]
        public ActionResult Get(string id)
        {
            id = HttpUtility.UrlDecode(id);
            byte[] response = _fssService.DownloadFile(id);
            MemoryStream stream = new MemoryStream(response, true);
            return File(stream, "application/octet-stream", id);
        }
    }
}