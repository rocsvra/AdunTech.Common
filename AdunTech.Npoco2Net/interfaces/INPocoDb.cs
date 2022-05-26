using System;

namespace AdunTech.Npoco2Net
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public interface INPocoDb : IDisposable, IDbQuery, IDbNonQuery
    {
        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 结束事务
        /// </summary>
        void CompleteTransaction();
    }
}
