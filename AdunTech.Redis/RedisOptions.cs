namespace AdunTech.Redis
{
    public class RedisOptions
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerHost { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 库
        /// </summary>
        public int Db { get; set; }
    }
}
