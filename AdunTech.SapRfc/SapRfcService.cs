using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdunTech.SapRfc
{
    public class SapRfcService
    {
        private readonly RfcDestination destination;

        private SapRfcService() { }

        /// <summary>
        /// SapRfc服务
        /// </summary>
        /// <param name="options">连接参数</param>
        public SapRfcService(SapConnectionOptions options)
        {
            //如果RFC管理器中不存在指定的目的地
            if (RfcDestinationManager.TryGetDestination(options.DestinationName) == null)
            {
                IDestinationConfiguration config = new SapDestinationConfiguration(options);
                RfcDestinationManager.RegisterDestinationConfiguration(config);    //则向RFC管理器注册目的地配置
            }
            destination = RfcDestinationManager.GetDestination(options.DestinationName);
        }

        /// <summary>
        /// Sap数据查询
        /// 内表名和返回类名保持一致
        /// </summary>
        /// <typeparam name="TOutput">响应类型</typeparam>
        /// <typeparam name="TInput">请求类型</typeparam>
        /// <param name="intput">请求参数</param>
        /// <param name="rfcFunctionName">rfc方法名</param>
        /// <returns></returns>
        public IEnumerable<TOutput> Query<TOutput, TInput>(TInput intput, string rfcFunctionName)
            where TInput : new()
            where TOutput : new()
        {
            IRfcFunction rfcFunc = InvokeRfc(intput, rfcFunctionName);
            return rfcFunc.Query<TOutput>();
        }

        /// <summary>
        /// Invoke方法
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="intput"></param>
        /// <param name="rfcFunctionName"></param>
        /// <returns></returns>
        public IRfcFunction InvokeRfc<TInput>(TInput intput, string rfcFunctionName)
        {
            var rfcFunc = destination.Repository.CreateFunction(rfcFunctionName);
            foreach (PropertyInfo pi in typeof(TInput).GetProperties())
            {
                string key = pi.Name;
                string value = pi.GetValue(intput)?.ToString();
                rfcFunc.SetValue(key, value);
            }
            rfcFunc.Invoke(destination);
            if (!"S".Equals(rfcFunc.GetValue("MSGTYPE")))
            {
                throw new Exception(rfcFunc.GetValue("MESSAGE").ToString());
            }
            return rfcFunc;
        }

        /// <summary>
        ///  Sap数据回写
        /// </summary>
        /// <typeparam name="TInput">回写参数类型</typeparam>
        /// <typeparam name="TOutput">响应参数类型</typeparam>
        /// <param name="input">回写参数</param>
        /// <param name="rfcFunctionName">回写函数名</param>
        /// <param name="innerTableName">回写内表名</param>
        /// <param name="outputTableName">返回信息表名</param>
        public IEnumerable<TOutput> ExecuteNonQuery<TOutput, TInput>(TInput input, string rfcFunctionName, string innerTableName, string outputTableName)
        {
            var rfcFunc = destination.Repository.CreateFunction(rfcFunctionName);
            IRfcTable rfcTable = rfcFunc.GetTable(innerTableName);
            rfcTable.Insert();

            foreach (PropertyInfo pi in input.GetType().GetProperties())
            {
                string key = pi.Name;
                string value = pi.GetValue(input)?.ToString();
                rfcTable.CurrentRow.SetValue(key, value);
            }
            rfcFunc.Invoke(destination);

            if (!"S".Equals(rfcFunc.GetValue("MSGTYPE")))
            {
                throw new Exception(rfcFunc.GetValue("MESSAGE").ToString());
            }
            IRfcTable rfcTbl = rfcFunc.GetTable(outputTableName);
            if (rfcTbl == null || rfcTbl.Count == 0)
            {
                return default(IEnumerable<TOutput>);
            }
            return rfcTbl.Query<TOutput>();
        }
    }
}
