using SAP.Middleware.Connector;
using System.Collections.Generic;

namespace AdunTech.SapRfc
{
    public interface ISapRfcService
    {
        /// <summary>
        /// Sap数据查询
        /// </summary>
        /// <typeparam name="TOutput">和内表名/数据结构保持一致</typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="intput"></param>
        /// <param name="rfcFunctionName">rfc方法名</param>
        /// <returns></returns>
        IEnumerable<TOutput> Query<TOutput, TInput>(TInput intput, string rfcFunctionName);

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="intput"></param>
        /// <param name="rfcFunctionName">rfc方法名</param>
        /// <returns></returns>
        IRfcFunction InvokeRfc4Query<TInput>(TInput intput, string rfcFunctionName);

        /// <summary>
        /// 回写数据（增删改）
        /// </summary>
        /// <typeparam name="TInput">回写实体名/数据结构保持一致</typeparam>
        /// <param name="input"></param>
        /// <param name="rfcFunctionName">rfc方法名</param>
        void ExecuteNonQuery<TInput>(TInput input, string rfcFunctionName);

        /// <summary>
        /// 批量回写数据（增删改）
        /// </summary>
        /// <typeparam name="TInput">回写实体名/数据结构保持一致</typeparam>
        /// <param name="input"></param>
        /// <param name="rfcFunctionName">rfc方法名</param>
        void ExecuteNonQuery<TInput>(IEnumerable<TInput> inputs, string rfcFunctionName);

        /// <summary>
        /// 回写数据（增删改）
        /// </summary>
        /// <typeparam name="TOutput">返回信息</typeparam>
        /// <typeparam name="TInput">回写实体名/数据结构保持一致</typeparam>
        /// <param name="input"></param>
        /// <param name="rfcFunctionName">rfc方法名</param>
        /// <returns></returns>
        IEnumerable<TOutput> ExecuteNonQuery<TOutput, TInput>(TInput input, string rfcFunctionName);

        /// <summary>
        /// 批量回写数据（增删改）
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="inputs"></param>
        /// <param name="rfcFunctionName">rfc方法名</param>
        /// <returns></returns>
        IEnumerable<TOutput> ExecuteNonQuery<TOutput, TInput>(IEnumerable<TInput> inputs, string rfcFunctionName);
    }
}
