using NPoco;
using System.Data.Common;

namespace AdunTech.Npoco2Net
{
    public partial class NPocoDb : INPocoDb
    {
        private Database db;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseType"></param>
        /// <param name="provider"></param>
        public NPocoDb(string connectionString, DatabaseType databaseType, DbProviderFactory provider)
        {
            db = new Database(connectionString, databaseType, provider);
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            db.BeginTransaction();
        }

        /// <summary>
        /// 结束事务
        /// </summary>
        public void CompleteTransaction()
        {
            db.CompleteTransaction();
        }

        /// <summary>
        /// 回收处理
        /// </summary>
        public void Dispose()
        {
            if (db != null)
            {
                db.KeepConnectionAlive = false;
                db.CloseSharedConnection();
            }
        }
    }
}
