using System.ComponentModel;

namespace AdunTech.IdcardOcr
{
    /// <summary>
    /// 图像识别状态
    /// </summary>
    public enum ImageStatus
    {
        /// <summary>
        /// 识别正常
        /// </summary>
        [Description("识别正常")]
        normal = 1,
        /// <summary>
        /// 身份证正反面颠倒
        /// </summary>
        [Description("身份证正反面颠倒")]
        reversed_side = 2,
        /// <summary>
        /// 上传的图片中不包含身份证
        /// </summary>
        [Description("上传的图片中不包含身份证")]
        non_idcard = 3,
        /// <summary>
        /// 身份证模糊
        /// </summary>
        [Description("身份证模糊")]
        blurred = 4,
        /// <summary>
        /// 其他类型证照
        /// </summary>
        [Description("其他类型证照")]
        other_type_card = 5,
        /// <summary>
        /// 身份证关键字段反光或过曝
        /// </summary>
        [Description("身份证关键字段反光或过曝")]
        over_exposure = 6,
        /// <summary>
        /// 身份证欠曝
        /// </summary>
        [Description("身份证欠曝")]
        over_dark = 7,
        /// <summary>
        /// 未知状态
        /// </summary>
        [Description("未知状态")]
        unknown = 8,
    }
}
