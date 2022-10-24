﻿namespace AdunTech.IdcardOcr
{
    /// <summary>
    /// https://ai.baidu.com/ai-doc/REFERENCE/Ck3dwjhhu
    /// </summary>
    public class TokenResult
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public string session_key { get; set; }
        public string session_secret { get; set; }
    }
}
