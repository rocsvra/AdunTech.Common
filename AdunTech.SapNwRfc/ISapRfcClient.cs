namespace AdunTech.SapNwRfc
{
    public interface ISapRfcClient
    {
        /// <summary>
        /// 执行SAP接口
        /// </summary>
        /// <typeparam name="TOutput">返回泛型</typeparam>
        /// <typeparam name="TInput">请求泛型</typeparam>
        /// <param name="input">请求参数</param>
        /// <param name="rfcFunctionName">rfc函数名</param>
        /// <returns></returns>
        TOutput Execute<TOutput, TInput>(TInput input, string rfcFunctionName);

        /// <summary>
        /// 执行SAP接口
        /// </summary>
        /// <typeparam name="TInput">请求泛型</typeparam>
        /// <param name="input">请求参数</param>
        /// <param name="rfcFunctionName">rfc函数名</param>
        void Execute<TInput>(TInput input, string rfcFunctionName);

        /// <summary>
        /// 执行SAP接口
        /// </summary>
        /// <param name="rfcFunctionName">rfc函数名</param>
        void Execute(string rfcFunctionName);
    }
}
