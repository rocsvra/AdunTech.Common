namespace AdunTech.IdcardOcr
{
    /// <summary>
    /// 正面信息（头像面）
    /// </summary>
    public class FrontSide
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdcardNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birth { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nationality { get; set; }
    }
}
