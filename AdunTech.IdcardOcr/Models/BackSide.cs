namespace AdunTech.IdcardOcr
{
    /// <summary>
    /// 身份证背面（国徽面）
    /// </summary>
    public class BackSide
    {
        /// <summary>
        /// 签发日期
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 失效日期
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 签发机构
        /// </summary>
        public string Issue { get; set; }
    }
}
