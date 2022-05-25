namespace AdunTech.SapRfc.Test
{
    /// <summary>
    /// 员工自助读写接口返回讯息
    /// </summary>
    public class ZHRI044A
    {
        public string PERNR { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public string ZIDNO { get; set; }
        /// <summary>
        /// 返回类型 E/S
        /// </summary>
        public string MSGTX { get; set; }
        /// <summary>
        /// 返回详情
        /// </summary>
        public string MSGTY { get; set; }
    }
}
