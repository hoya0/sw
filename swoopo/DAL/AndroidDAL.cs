using System;
using System.Collections;
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
    /// 机器人
    /// </summary>
    public class AndroidDal : DalBase
    {
        private const string tableName = "t_android";

        public int Insert(AndroidEntity android)
        {
            try
            {
                string sql = "insert into " + tableName + "([UserName],[State]) values(@userName,@state)";

                System.Data.Common.DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "userName", DbType.String, android.UserName);
                db.AddInParameter(cmd, "state", DbType.Byte, android.State);

                int result = db.ExecuteNonQuery(cmd);
                return result;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

        public int Update(AndroidEntity android)
        {
            return 0;
        }

        public int Delete(AndroidEntity android)
        {
            return 0;
        }

        public List<AndroidEntity> GetByPaged(AndroidEntity android, PagerCondition pagerCondition)
        {
            try
            {
                pagerCondition.SelectFields = "ID,UserName,State";

                System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("pagination");
                db.AddInParameter(cmd, "tblName", DbType.String, tableName);
                db.AddInParameter(cmd, "strGetFields", DbType.String, pagerCondition.SelectFields);
                db.AddInParameter(cmd, "fldName", DbType.String, pagerCondition.SortField);//排序字段名
                db.AddInParameter(cmd, "PageSize", DbType.Int32, pagerCondition.PageSize);
                db.AddInParameter(cmd, "PageIndex", DbType.Int32, pagerCondition.PageIndex);
                db.AddInParameter(cmd, "doCount", DbType.Byte, pagerCondition.doCount);
                db.AddInParameter(cmd, "OrderType", DbType.Byte, pagerCondition.SortType);
                db.AddInParameter(cmd, "strWhere", DbType.String, pagerCondition.StrWhere);

                List<AndroidEntity> list = new List<AndroidEntity>();

                string[] fields = pagerCondition.SelectFields.Split(new char[1] { ',' });

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        list.Add(new AndroidEntity
                        {
                            ID = Convert.ToInt32(reader[0]),
                            UserName = reader[1].ToString(),
                            State = Convert.ToByte(reader[2])
                        });
                    }
                    reader.Close();
                    reader.Dispose();
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

        public List<AndroidEntity> GetByCondition(Dictionary<string, string> conditionDic)
        {
            try
            {
                if (conditionDic == null)
                {
                    return null;
                }

                string fileds = "ID,UserName,State";

                StringBuilder sql = new StringBuilder("select ").Append(fileds).Append(" from ").Append(tableName).Append(" where 1=1");
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

                List<AndroidEntity> list = new List<AndroidEntity>();
                using (IDataReader reader = db.ExecuteReader(System.Data.CommandType.Text, sql.ToString()))
                {
                    while (reader.Read())
                    {
                        list.Add(new AndroidEntity
                        {
                            ID = Convert.ToInt32(reader[0]),
                            UserName = reader[1].ToString(),
                            State = Convert.ToByte(reader[2])
                        });
                    }
                    reader.Close();
                    reader.Dispose();
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new DalException("系统异常！", ex);
            }
        }

    } //end class
}//end namespace
