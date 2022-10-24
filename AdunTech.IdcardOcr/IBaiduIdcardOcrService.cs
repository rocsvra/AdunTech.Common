namespace AdunTech.IdcardOcr
{
    /// <summary>
    /// 百度身份证识别
    /// </summary>
    public interface IBaiduIdcardOcrService
    {
        /// <summary>
        /// 获取口令
        /// </summary>
        /// <returns></returns>
        TokenResult GetAccesstoken();
        /// <summary>
        /// 身份证背面（国徽）
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        BackSide GetBackInfo(byte[] image);
        /// <summary>
        /// 身份证正面（人脸）
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        FrontSide GetFrontInfo(byte[] image);
    }
}
