namespace AdunTech.SapRfc
{
    /// <summary>
    /// sap连接参数
    /// </summary>
    public class SapConnectionOptions
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string SystemNumber { get; set; }
        public string AppServerHost { get; set; }
        public string Client { get; set; }
        public string Language { get; set; }
        public string PoolSize { get; set; }
        public string MaxPoolSize { get; set; }
        public string IdleTimeout { get; set; }

        /// <summary>
        /// rfc目的地名称
        /// </summary>
        public string DestinationName { get; set; }
    }
}
