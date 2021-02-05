using System;
using System.IO;

namespace AdunTech.Co2Net.Helpers
{
    /// <summary>
    /// 文件处理
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static FileStream GetFileStream(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("文件路径为空");
            }
            if (!File.Exists(path))
            {
                throw new Exception("文件不存在");
            }
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
    }
}
