using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using Swoopo.Model;
using Swoopo.ExceptionHandler;

namespace Swoopo.DAL
{
    /// <summary>
    /// 用户充值历史
    /// </summary>
    public class UserBalanceHistoryDal : DalBase
    {
        private static string tableName = "t_user_balance_history";

        public int Insert(UserBalanceHistoryEntity userBalanceHistory)
        {
            try
            {
                string sql = "insert into " + tableName + "([UserID],[Balance],[Remark])" +
                    " values(@userId,@balance,@remark)";

                System.Data.Common.DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "userId", DbType.String, userBalanceHistory.UserID);
                db.AddInParameter(cmd, "balance", DbType.DateTime, userBalanceHistory.Balance);
                db.AddInParameter(cmd, "remark", DbType.DateTime, userBalanceHistory.Remark);

                int result = 1;
                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    DbTransaction tran = conn.BeginTransaction();
                    try
                    {
                        db.ExecuteNonQuery(cmd, tran);
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        result = 0;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

        /// <summary>
        /// 充值历史不提供修改的功能
        /// </summary>
        /// <param name="product"></param>
        /// <param name="conditionDic"></param>
        /// <returns></returns>
        public int Update(UserPriceProductEntity userBalanceHistory, Dictionary<string, string> conditionDic)
        {
            //if (conditionDic == null)
            //{
            //    return 0;
            //}

            //StringBuilder sql = new StringBuilder("Update ").Append(tableName);
            //sql.Append(" set ");
            //sql.Append("UserID=@uid,");
            //sql.Append("Balance=@balance,");
            //sql.Append("Remark=@remark");

            //int i = 0, len = conditionDic.Keys.Count - 1;

            //foreach (KeyValuePair<string, string> item in conditionDic)
            //{
            //    if (i == len)
            //    {
            //        sql.Append(item.Key).Append("=").Append(item.Value);
            //    }
            //    else
            //    {
            //        sql.Append(item.Key).Append("=").Append(item.Value).Append(" and ");
            //    }
            //}

            //int result = db.ExecuteNonQuery(CommandType.Text, sql.ToString());
            //return result;
            return 0;
        }

        public int Delete(UserPriceProductEntity userBalanceHistory, Dictionary<string, string> conditionDic)
        {
            try
            {
                if (conditionDic == null)
                {
                    return 0;
                }
                StringBuilder sql = new StringBuilder("Delete from ").Append(tableName);
                int i = 0, len = conditionDic.Keys.Count - 1;
                foreach (KeyValuePair<string, string> item in conditionDic)
                {
                    if (i == len)
                    {
                        sql.Append(item.Key).Append("=").Append(item.Value);
                    }
                    else
                    {
                        sql.Append(item.Key).Append("=").Append(item.Value).Append(" and ");
                    }
                }

                int result = 1;
                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    DbTransaction tran = conn.BeginTransaction();
                    try
                    {
                        db.ExecuteNonQuery(tran, CommandType.Text, sql.ToString());
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        result = 0;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

        public List<UserPriceProductEntity> GetByPaged(ProductEntity product, PagerCondition pagerCondition)
        {
            try
            {
                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("pagination");
                db.AddInParameter(cmd, "tblName", DbType.String, tableName);
                db.AddInParameter(cmd, "strGetFields", DbType.String, pagerCondition.SelectFields);
                db.AddInParameter(cmd, "fldName", DbType.String, pagerCondition.SortField);//排序字段名
                db.AddInParameter(cmd, "PageSize", DbType.Int32, pagerCondition.PageSize);
                db.AddInParameter(cmd, "PageIndex", DbType.Int32, pagerCondition.PageIndex);
                db.AddInParameter(cmd, "doCount", DbType.Byte, pagerCondition.doCount);
                db.AddInParameter(cmd, "OrderType", DbType.Byte, pagerCondition.SortType);
                db.AddInParameter(cmd, "strWhere", DbType.String, pagerCondition.StrWhere);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    reader.Close();
                    reader.Dispose();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

        public List<UserBalanceHistoryEntity> GetByCondition(Dictionary<string, string> conditionDic)
        {
            try
            {
                if (conditionDic == null)
                {
                    return null;
                }
                StringBuilder sql = new StringBuilder("select * from ").Append(tableName).Append(" where 1=1");
                int i = 0, len = conditionDic.Keys.Count - 1;

                foreach (KeyValuePair<string, string> item in conditionDic)
                {
                    if (i == len)
                    {
                        sql.Append(item.Key).Append("=").Append(item.Value);
                    }
                    else
                    {
                        sql.Append(item.Key).Append("=").Append(item.Value).Append(" and ");
                    }
                }

                using (IDataReader reader = 
                    db.ExecuteReader(System.Data.CommandType.Text, sql.ToString()))
                {
                    reader.Close();
                    reader.Dispose();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

    }//end class
}
