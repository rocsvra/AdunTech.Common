using Baidu.Aip.Ocr;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace AdunTech.IdcardOcr
{
    /// <summary>
    /// 百度身份识别
    /// </summary>
    public class BaiduIdcardOcrService : IBaiduIdcardOcrService
    {
        private Ocr _ocrClient => new Ocr(_clientId, _clientSecret);
        private string _clientId { get; set; }
        private string _clientSecret { get; set; }

        public BaiduIdcardOcrService(BaiduOcrOptions options)
        {
            _clientId = options.ApiKey;
            _clientSecret = options.SecretKey;
            _ocrClient.Timeout = 60 * 1000;
        }

        public BaiduIdcardOcrService(IOptions<BaiduOcrOptions> options)
        {
            _clientId = options.Value.ApiKey;
            _clientSecret = options.Value.SecretKey;
            _ocrClient.Timeout = 60 * 1000;
        }

        /// <summary>
        /// 口令
        /// </summary>
        /// <returns></returns>
        public TokenResult GetAccesstoken()
        {
            using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
            {
                using (var client = new HttpClient(handler))
                {
                    List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("grant_type", "client_credentials"),
                        new KeyValuePair<string, string>("client_id", _clientId),
                        new KeyValuePair<string, string>("client_secret", _clientSecret)
                    };

                    HttpResponseMessage response = client.PostAsync("https://aip.baidubce.com/oauth/2.0/token", new FormUrlEncodedContent(param)).Result;
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var err = JsonConvert.DeserializeObject<TokenErrorResult>(result);
                        string errMsg = string.Format("StatusCode:{0};error:{1};error_description:{2}", response.StatusCode, err.error, err.error_description);
                        throw new Exception(errMsg);
                    }
                    return JsonConvert.DeserializeObject<TokenResult>(result);
                }
            }
        }

        /// <summary>
        /// 身份证背面（国徽）
        /// </summary>
        /// <param name="image"></param>
        public BackSide GetBackInfo(byte[] image)
        {
            var ocrResult = _ocrClient.Idcard(image, "back");
            string imageStatus = ocrResult["image_status"].ToString();
            if (imageStatus != Enum.GetName(typeof(ImageStatus), ImageStatus.normal))
            {
                throw new IdcardOcrException(imageStatus);
            }
            return new BackSide
            {
                StartDate = ocrResult["words_result"]["签发日期"]["words"].ToString(),
                EndDate = ocrResult["words_result"]["失效日期"]["words"].ToString(),
                Issue = ocrResult["words_result"]["签发机关"]["words"].ToString()
            };
        }

        /// <summary>
        /// 身份证正面（头像）
        /// </summary>
        /// <param name="image"></param>
        public FrontSide GetFrontInfo(byte[] image)
        {
            var ocrResult = _ocrClient.Idcard(image, "front");
            string imageStatus = ocrResult["image_status"].ToString();
            if (imageStatus != Enum.GetName(typeof(ImageStatus), ImageStatus.normal))
            {
                throw new IdcardOcrException(imageStatus);
            }
            return new FrontSide
            {
                Address = ocrResult["words_result"]["住址"]["words"].ToString(),
                IdcardNo = ocrResult["words_result"]["公民身份号码"]["words"].ToString(),
                Name = ocrResult["words_result"]["姓名"]["words"].ToString(),
                Sex = ocrResult["words_result"]["性别"]["words"].ToString(),
                Birth = ocrResult["words_result"]["出生"]["words"].ToString(),
                Nationality = ocrResult["words_result"]["民族"]["words"].ToString()
            };
        }
    }
}
