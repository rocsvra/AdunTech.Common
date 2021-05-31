using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

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

        private static ISapRfcService srv = new SapRfcService(cfg);

        static void Main(string[] args)
        {
            //SingleQuery();
            //MulQuery();
            //WriteBack();
            WriteBackMul();
            Console.ReadLine();
        }

        /// <summary>
        /// 单表
        /// </summary>
        static void SingleQuery()
        {
            var input = new ZHRI043D_Input { IM_PERNR = "00228028" };
            var data = srv.Query<ZHRI043D, ZHRI043D_Input>(input, "ZHRI043D");

            Console.WriteLine(JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// 结果集
        /// </summary>
        static void MulQuery()
        {
            var input = new ZHRI043_Input { IM_PERNR = "00228028", IM_MONTH = "202011" };
            var func = srv.InvokeRfc4Query(input, "ZHRI043"); //执行sap远程函数
            ZHRI043_Output data = new ZHRI043_Output
            {
                ZHRI043A = func.Query<ZHRI043A>(),//获取数据
                ZHRI043B = func.Query<ZHRI043B>(),
                ZHRI043E = func.Query<ZHRI043E>(),
            };

            Console.WriteLine(JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// 回写
        /// </summary>
        static void WriteBackMul()
        {
            List<ZZT0185> cards = new List<ZZT0185>();
            ZZT0185 idCard1 = new ZZT0185();
            idCard1.BEGDA = DateTime.Now.ToString("yyyyMMdd");
            idCard1.ENDDA = "99991231";
            idCard1.ICNUM = "330281198411104114";
            idCard1.LOCAT = "宁波";
            idCard1.PERNR = "00190330";
            idCard1.USEFR = "20201112";
            idCard1.USETO = "20241010";
            idCard1.ZIDNO = "00190330";

            ZZT0185 idCard2 = new ZZT0185();
            idCard2.BEGDA = DateTime.Now.ToString("yyyyMMdd");
            idCard2.ENDDA = "99991231";
            idCard2.ICNUM = "330281198411104114";
            idCard2.LOCAT = "宁波";
            idCard2.PERNR = "00190330";
            idCard2.USEFR = "20201112";
            idCard2.USETO = "20241010";
            idCard2.ZIDNO = "00228028";
            cards.Add(idCard1);
            cards.Add(idCard2);

            IEnumerable<ZHRI044A> data = srv.ExecuteNonQuery<ZHRI044A, ZZT0185>(cards, "ZHRI044");
            Console.WriteLine(JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// 回写
        /// </summary>
        static void WriteBack()
        {
            ZZT0185 input = new ZZT0185()
            {                 
                BEGDA = DateTime.Now.ToString("yyyyMMdd"),
                ENDDA = "99991231",
                ICNUM = "330281198411104114",
                LOCAT = "宁波",
                PERNR = "00190330",
                USEFR = "20201112",
                USETO = "20241010",
                ZIDNO = "00190330",
            };
            srv.ExecuteNonQuery(input, "ZHRI044");
        }
    }
}
