using System.Collections.Generic;

namespace AdunTech.Npoco2Net
{
    /// <summary>
    /// NonQuery
    /// </summary>
    public partial class NPocoDb : IDbNonQuery
    {
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, params object[] args)
        {
            return db.Execute(sql, args);
        }

        /// <summary>
        /// 批量执行sql
        /// </summary>
        /// <param name="sqls"></param>
        public void ExecuteNonQuery(IEnumerable<string> sqls)
        {
            db.BeginTransaction();
            foreach (var sql in sqls)
            {
                db.Execute(sql);
            }
            db.CompleteTransaction();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="poco"></param>
        /// <returns></returns>
        public object Insert<T>(T poco)
        {
            return db.Insert(poco);
        }

        /// <summary>
        /// 插入(批量)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pocos"></param>
        /// <returns></returns>
        public int InsertBatch<T>(IEnumerable<T> pocos)
        {
            return db.InsertBatch(pocos);
        }

        /// <summary>
        /// 插入(巨量)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pocos"></param>
        public void InsertBulk<T>(IEnumerable<T> pocos)
        {
            db.InsertBulk(pocos);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public int Update(object poco)
        {
            return db.Update(poco);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public int Delete(object poco)
        {
            return db.Delete(poco);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pocoOrPrimaryKey">实体或主键</param>
        /// <returns></returns>
        public int Delete<T>(object pocoOrPrimaryKey)
        {
            return db.Delete<T>(pocoOrPrimaryKey);
        }
    }
}
