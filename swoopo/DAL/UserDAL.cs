using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using Swoopo.Model;
using Swoopo.ExceptionHandler;

namespace Swoopo.DAL
{
    public class UserDAL : DalBase
    {
        private const string tableName = "t_user";

        public UserEntity Signin(UserEntity user, Dictionary<string,string> condition)
        {
            if (condition == null)
            {
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ").Append(tableName).Append(" where ");

            sql.Append(DalBase.ParseCondition(condition));

            UserEntity userResult = null;
            try
            {
                using (IDataReader reader = db.ExecuteReader(CommandType.Text, sql.ToString()))
                {
                    while (reader.Read())
                    {
                        userResult = new UserEntity
                        {
                            ID = Convert.ToInt32(reader[0]),
                            UserName = reader[1].ToString(),
                            UserPwd = reader[2].ToString(),
                            Email = reader[3].ToString(),
                            Balance = Convert.ToDecimal(reader[4]),
                            UserState = Convert.ToByte(reader[5]),
                            UserType = Convert.ToByte(reader[6]),
                            CreateTime = Convert.ToDateTime(reader[7])
                        };
                    }
                    reader.Close();
                    reader.Dispose();
                }
                return userResult;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

        public int Insert(ProductEntity product)
        {
            string sql = "insert into " + tableName + "([ProName],[StarTime],[EndTime],[ProState],[Remark],[RemainTime],[CategoryID])" +
                " values(@productName,@starTime,@entTime,@proState,@remark,@remainTime,@categoryId)";

            int result = 0;
            try
            {
                System.Data.Common.DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "productName", DbType.String, product.ProName);
                db.AddInParameter(cmd, "starTime", DbType.DateTime, product.StarTime);
                db.AddInParameter(cmd, "entTime", DbType.DateTime, product.EndTime);
                db.AddInParameter(cmd, "proState", DbType.Byte, product.ProState);
                db.AddInParameter(cmd, "remark", DbType.String, product.Remark);
                db.AddInParameter(cmd, "remainTime", DbType.Int32, product.RemainTime);
                db.AddInParameter(cmd, "categoryId", DbType.Int32, product.CategoryID);

                result = db.ExecuteNonQuery(cmd);
                return result;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

        public int Update(UserEntity user, Dictionary<string,string> conditionDic)
        {
            if (conditionDic == null)
            {
                return 0;
            }

            StringBuilder sql = new StringBuilder("Update ").Append(tableName);
            sql.Append(" set ");
            sql.Append("UserPwd=@pwd,");
            sql.Append("Email=@main,");
            sql.Append("Balance=@balance,");
            sql.Append("UserState=@state,");
            sql.Append("UserType=@type where ");

            sql.Append(DalBase.ParseCondition(conditionDic));

            int result = 1;
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
                    cmd.Transaction = tran;
                    db.AddInParameter(cmd, "pwd", DbType.String, user.UserPwd);
                    db.AddInParameter(cmd, "Email", DbType.String, user.Email);
                    db.AddInParameter(cmd, "balance", DbType.Decimal, user.Balance);
                    db.AddInParameter(cmd, "state", DbType.Byte, user.UserState);
                    db.AddInParameter(cmd, "type", DbType.Byte, user.UserType);

                    db.ExecuteNonQuery(cmd);
                    tran.Commit();
                    return result;
                }
                catch (Exception ex)
                { 
                    tran.Rollback();
                    result = 0;
                    throw new DalException("系统异常！", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int Delete(ProductEntity product, Dictionary<string,string> conditionDic)
        {
            int result = 0;
            try
            {
                if (conditionDic == null)
                {
                    return 0;
                }
                StringBuilder sql = new StringBuilder("Delete from ").Append(tableName);
                sql.Append(DalBase.ParseCondition(conditionDic));
                result = db.ExecuteNonQuery(CommandType.Text, sql.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

        public List<ProductEntity> GetByPaged(ProductEntity product, PagerCondition pagerCondition)
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
                throw new Exception("系统异常！", ex);
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
                sql.Append(DalBase.ParseCondition(conditionDic));

                using (IDataReader reader = db.ExecuteReader(System.Data.CommandType.Text, sql.ToString()))
                {
                    reader.Close();
                    reader.Dispose();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new DalException("系统异常！", e);
            }
        }

    }//end class
}
