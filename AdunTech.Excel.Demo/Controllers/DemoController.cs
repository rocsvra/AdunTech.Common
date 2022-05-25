using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using AdunTech.Extension;

namespace AdunTech.Excel.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        private readonly IExcelService _excelService;

        public DemoController(ILogger<DemoController> logger, IExcelService excelService)
        {
            _logger = logger;
            _excelService = excelService;
        }

        /// <summary>
        /// 导入测试
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("Import")]
        public IActionResult Import(IFormFile file)
        {
            DataTable dt = null;
            string extension = Path.GetExtension(file.FileName);

            using (Stream fileStream = file.OpenReadStream())
            {
                dt = _excelService.Import(new ImportOptions
                {
                    Ext = extension,
                    File = fileStream
                });
            }

            foreach (DataRow row in dt.Rows)
            {
                _logger.LogInformation(row["工号"].ToString().PadLeft(8, '0'));
            }
            return Ok();
        }

        /// <summary>
        /// 导出测试
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("Export")]
        public IActionResult Export()
        {
            List<Test> data = new List<Test>
            {
                new Test{ Product="c1", Version="1.1",Desc="d1"},
                new Test{ Product="c2", Version="1.2",Desc="d2"},
            };

            ExportOptions options = new ExportOptions
            {
                DataScource = data.ToDataTable(),
                ReportName = "测试报表名称",
                DataColumnMap = new Dictionary<string, string>
                {
                    { "Product","产品" },
                    { "Version","版本" },
                }
            };

            MemoryStream stream = _excelService.Export(options);
            string contentType = "application/vnd.ms-excel";
            string fileDownloadName = "ExcelTest.xls";
            return File(stream, contentType, fileDownloadName);
        }
    }
}
