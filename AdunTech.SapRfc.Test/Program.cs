using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdunTech.SapRfc.Test
{
    class Program
    {
        private readonly static SapConnectionOptions cfg = new SapConnectionOptions
        {
            User = ConfigurationManager.AppSettings["SapConnection:User"],
            Password = ConfigurationManager.AppSettings["SapConnection:Password"],
            SystemNumber = ConfigurationManager.AppSettings["SapConnection:SystemNumber"],
            AppServerHost = ConfigurationManager.AppSettings["SapConnection:AppServerHost"],
            Client = ConfigurationManager.AppSettings["SapConnection:Client"],
            Language = ConfigurationManager.AppSettings["SapConnection:Language"],
            PoolSize = ConfigurationManager.AppSettings["SapConnection:PoolSize"],
            MaxPoolSize = ConfigurationManager.AppSettings["SapConnection:MaxPoolSize"],
            IdleTimeout = ConfigurationManager.AppSettings["SapConnection:IdleTimeout"],
            DestinationName = ConfigurationManager.AppSettings["SapConnection:DestinationName"],
        };

        private static SapRfcService srv = new SapRfcService(cfg);

        static void Main(string[] args)
        {
            Single();
            Mul();
            Console.ReadLine();
        }

        /// <summary>
        /// 单表
        /// </summary>
        static void Single()
        {
            var input = new ZHRI043D_Input { IM_PERNR = "00228028" };
            var data = srv.Query<ZHRI043D, ZHRI043D_Input>(input, "ZHRI043D");
            Console.WriteLine(JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// 结果集
        /// </summary>
        static void Mul()
        {
            var input = new ZHRI043_Input { IM_PERNR = "00228028", IM_MONTH = "202011" };
            var func = srv.InvokeRfc(input, "ZHRI043");
            ZHRI043_Output data = new ZHRI043_Output
            {
                ZHRI043A = func.Query<ZHRI043A>(),
                ZHRI043B = func.Query<ZHRI043B>(),
                ZHRI043E = func.Query<ZHRI043E>(),
            };
            Console.WriteLine(JsonConvert.SerializeObject(data));
        }
    }
}
