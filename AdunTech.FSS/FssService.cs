using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Zaabee.FastDfsClient;
using Zaabee.FastDfsClient.Common;

namespace AdunTech.FSS
{
    /// <summary>
    /// 文件存储服务
    /// </summary>
    public class FssService : IFssService
    {
        private readonly FssOptions _options;
        private FastDfsClient _fssClient;
        private StorageNode _storageNode;

        public FssService(IOptions<FssOptions> options)
        {
            _options = options.Value;
            _fssClient = new FastDfsClient(new List<IPEndPoint>
            {
                new IPEndPoint(IPAddress.Parse(_options.EndPoint),_options.Port)
            });
            _storageNode = _fssClient.GetStorageNode(_options.GroupName);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">文件服务器地址</param>
        /// <param name="endPoint">终端，exp：172.16.2.27</param>
        /// <param name="port">端口：exp：22122</paramFssOptions
        public FssService(string endPoint, int port, string groupName)
        {
            _options = new FssOptions
            {
                EndPoint = endPoint,
                Port = port,
                GroupName = groupName,
            };
            _fssClient = new FastDfsClient(new List<IPEndPoint>
            {
                new IPEndPoint(IPAddress.Parse(endPoint),port)
            });
            _storageNode = _fssClient.GetStorageNode(_options.GroupName);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件id</param>
        public byte[] DownloadFile(string fileName)
        {
            return _fssClient.DownloadFile(_storageNode, fileName);
        }

        /// <summary>
        /// 上传文件到OSS服务器
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="fileExt">后缀名</param>
        /// <returns>返回文件id</returns>
        public string UploadFile(Stream fileStream, string fileExt)
        {
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            return _fssClient.UploadFile(_storageNode, buffer, fileExt);
        }

        /// <summary>
        /// 上传文件到OSS服务器
        /// </summary>
        /// <param name="fileByte">文件byte[]</param>
        /// <param name="fileExt">后缀名</param>
        /// <returns>返回文件id</returns>
        public string UploadFile(byte[] fileByte, string fileExt)
        {
            return _fssClient.UploadFile(_storageNode, fileByte, fileExt);
        }

        /// <summary>
        /// 移除文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void RemoveFile(string fileName)
        {
            _fssClient.RemoveFile(_options.GroupName, fileName);
        }
    }
}
