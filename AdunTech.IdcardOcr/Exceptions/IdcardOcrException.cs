using System;

namespace AdunTech.IdcardOcr
{
    /// <summary>
    /// 身份证识别异常
    /// </summary>
    public class IdcardOcrException : Exception
    {
        /// <summary>
        /// 图像识别状态
        /// </summary>
        public string ImageStatus { get; }

        public IdcardOcrException(string imageStatus) : base(GetMessage(imageStatus))
        {
            ImageStatus = imageStatus;
        }

        private static string GetMessage(string imageStatus)
        {
            ImageStatus status = (ImageStatus)Enum.Parse(typeof(ImageStatus), imageStatus);
            return status.GetDescription();
        }
    }
}
