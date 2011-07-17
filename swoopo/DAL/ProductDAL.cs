using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using Swoopo.Model;
using Swoopo.ExceptionHandler;

namespace Swoopo.DAL
{
    public class ProductDal : DalBase
    {
        private static string tableName = "t_product";

        public int Insert(ProductEntity product)
        {
            try
            {
                string sql = "insert into " + tableName + "([ProName],[StarTime],[EndTime],[ProState],[Remark],[RemainTime],[CategoryID])" +
                    " values(@productName,@starTime,@entTime,@proState,@remark,@remainTime,@categoryId)";

                System.Data.Common.DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "productName", DbType.String, product.ProName);
                db.AddInParameter(cmd, "starTime", DbType.DateTime, product.StarTime);
                db.AddInParameter(cmd, "entTime", DbType.DateTime, product.EndTime);
                db.AddInParameter(cmd, "proState", DbType.Byte, product.ProState);
                db.AddInParameter(cmd, "remark", DbType.String, product.Remark);
                db.AddInParameter(cmd, "remainTime", DbType.Int64, product.RemainTime);
                db.AddInParameter(cmd, "categoryId", DbType.Int32, product.CategoryID);

                int result = db.ExecuteNonQuery(cmd);
                return result;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

        public int Update(ProductEntity product,Dictionary<string,string> conditionDic)
        {
            try
            {
                if (conditionDic == null)
                {
                    return 0;
                }

                StringBuilder sql = new StringBuilder("Update ").Append(tableName);
                sql.Append(" set ");
                sql.Append("ProName=@name,");
                sql.Append("StarTime=@startime,");
                sql.Append("EndTime=@endtime,");
                sql.Append("ProState=@state,");
                sql.Append("Remark=@remark,");
                sql.Append("RemainTime=@remainTime");
                sql.Append("CategoryID=@categoryId where ");

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

                DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
                db.AddInParameter(cmd, "name", DbType.String, product.ProName);
                db.AddInParameter(cmd, "startime", DbType.DateTime, product.StarTime);
                db.AddInParameter(cmd, "endtime", DbType.DateTime, product.EndTime);
                db.AddInParameter(cmd, "state", DbType.Byte, product.ProState);
                db.AddInParameter(cmd, "remark", DbType.String, product.Remark);
                db.AddInParameter(cmd, "remainTime", DbType.Int64, product.RemainTime);
                db.AddInParameter(cmd, "categoryId", DbType.Int32, product.CategoryID);

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

        public int Delete(ProductEntity product, Dictionary<string,string> conditionDic)
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
                int result = db.ExecuteNonQuery(CommandType.Text, sql.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

        public List<ProductEntity> GetByPaged(PagerCondition pagerCondition)
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

        public List<UserEntity> GetByCondition(Dictionary<string, string> conditionDic)
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
