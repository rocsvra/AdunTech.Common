using AdunTech.SapNwRfc.Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SapNwRfc.Pooling;
using System.Collections.Generic;

namespace AdunTech.SapNwRfc.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ISapRfcClient _sapClient;

        public TestController(ILogger<TestController> logger, ISapRfcClient sapClient)
        {
            _logger = logger;
            _sapClient = sapClient;
        }

        /// <summary>
        /// 单表
        /// </summary>
        /// <returns></returns>
        [HttpGet("Single")]
        public ActionResult GetSingle()
        {
            var input = new ZHRI043D_Input { IM_PERNR = "00228028" };
            var output = _sapClient.Execute<ZHRI043D_Output, ZHRI043D_Input>(input, "ZHRI043D");
            return Ok(output);
        }

        /// <summary>
        /// 多表
        /// </summary>
        /// <returns></returns>
        [HttpGet("Mul")]
        public ActionResult GetMul()
        {
            var input = new ZHRI043_Input { IM_PERNR = "00228028", IM_MONTH = "202011" };
            var output = _sapClient.Execute<ZHRI043_Output, ZHRI043_Input>(input, "ZHRI043");
            return Ok(output);
        }

        /// <summary>
        /// 回写
        /// </summary>
        /// <returns></returns>
        [HttpPut("WriteBack")]
        public ActionResult Modify()
        {
            List<ZZT0185> cards = new List<ZZT0185>();

            ZZT0185 idCard = new ZZT0185();
            idCard.BEGDA = System.DateTime.Now.ToString("yyyyMMdd");
            idCard.ENDDA = "99991231";
            idCard.ICNUM = "330281198411104114";
            idCard.LOCAT = "余姚";
            idCard.PERNR = "00190330";
            idCard.USEFR = "20201112";
            idCard.USETO = "20241010";
            idCard.ZIDNO = "00190330";

            cards.Add(idCard);

            ZZT0185_Input input = new ZZT0185_Input()
            {
                 ZZT0185 = cards.ToArray()
            };
            SapOutputBase output = _sapClient.Execute<SapOutputBase, ZZT0185_Input>(input, "ZHRI044");
            return Ok(output);
        }
    }
}
