using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdunTech.SapRfc
{
    public class SapRfcService : ISapRfcService
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
        {
            IRfcFunction rfcFunc = InvokeRfc4Query(intput, rfcFunctionName);
            return rfcFunc.Query<TOutput>();
        }

        /// <summary>
        /// Invoke方法
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="intput"></param>
        /// <param name="rfcFunctionName"></param>
        /// <returns></returns>
        public IRfcFunction InvokeRfc4Query<TInput>(TInput intput, string rfcFunctionName)
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
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="input">回写实体</param>
        /// <param name="rfcFunctionName">回写函数名</param>
        public IEnumerable<TOutput> ExecuteNonQuery<TOutput, TInput>(TInput input, string rfcFunctionName)
        {
            string outputTableName = typeof(TOutput).Name;
            var rfcFunc = InvokeRfc4NonQuery(input, rfcFunctionName);
            return rfcFunc.GetTable(outputTableName).Query<TOutput>();
        }

        public void ExecuteNonQuery<TInput>(TInput input, string rfcFunctionName)
        {
            InvokeRfc4NonQuery(input, rfcFunctionName);
        }

        public void ExecuteNonQuery<TInput>(IEnumerable<TInput> inputs, string rfcFunctionName)
        {
            InvokeRfc4NonQuery(inputs, rfcFunctionName);
        }

        /// <summary>
        /// Sap数据回写(批量)
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="inputs">回写实体</param>
        /// <param name="rfcFunctionName"></param>
        /// <returns></returns>
        public IEnumerable<TOutput> ExecuteNonQuery<TOutput, TInput>(IEnumerable<TInput> inputs, string rfcFunctionName)
        {
            string outputTableName = typeof(TOutput).Name;
            var rfcFunc = InvokeRfc4NonQuery(inputs, rfcFunctionName);
            return rfcFunc.GetTable(outputTableName).Query<TOutput>();
        }

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="rfcFunctionName"></param>
        /// <returns></returns>
        private IRfcFunction InvokeRfc4NonQuery<TInput>(TInput input, string rfcFunctionName)
        {
            IEnumerable<TInput> inputs = new List<TInput> { input };
            return InvokeRfc4NonQuery(inputs, rfcFunctionName);
        }

        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="inputs"></param>
        /// <param name="rfcFunctionName"></param>
        /// <returns></returns>
        private IRfcFunction InvokeRfc4NonQuery<TInput>(IEnumerable<TInput> inputs, string rfcFunctionName)
        {
            var innerTableName = typeof(TInput).Name;
            var rfcFunc = destination.Repository.CreateFunction(rfcFunctionName);

            IRfcTable rfcTable = rfcFunc.GetTable(innerTableName);
            rfcTable.Insert();
            foreach (var input in inputs)
            {
                foreach (PropertyInfo pi in input.GetType().GetProperties())
                {
                    string key = pi.Name;
                    string value = pi.GetValue(input)?.ToString();
                    rfcTable.CurrentRow.SetValue(key, value);
                }
            }
            rfcFunc.Invoke(destination);

            if (!"S".Equals(rfcFunc.GetValue("MSGTYPE")))
            {
                throw new Exception(rfcFunc.GetValue("MESSAGE").ToString());
            }
            return rfcFunc;
        }
    }
}
