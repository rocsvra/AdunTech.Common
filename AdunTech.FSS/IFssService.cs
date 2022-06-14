using System.IO;

namespace AdunTech.FSS
{
    public interface IFssService
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        byte[] DownloadFile(string path);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="fileExt">后缀名</param>
        /// <returns>返回文件存储相对路径</returns>
        string UploadFile(Stream fileStream, string fileExt);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileByte">文件byte[]</param>
        /// <param name="fileExt">后缀名</param>
        /// <returns>返回文件存储相对路径</returns>
        string UploadFile(byte[] fileByte, string fileExt);
    }
}
