using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Swoopo.Model;

using Microsoft.Practices.EnterpriseLibrary.Caching;
using Swoopo.ExceptionHandler;

namespace Swoopo.DAL
{
    /// <summary>
    /// 商品分类
    /// </summary>
    public class CategoryDAL : DalBase
    {
        private string CacheKey = "CATEGORIES";
        private readonly ICacheManager _cacheManager = CacheFactory.GetCacheManager();

        /// <summary>
        /// 插入分类
        /// </summary>
        /// <param name="categoryEntity"></param>
        public int Insert(Model.CategoryEntity categoryEntity)
        {
            var sql = new StringBuilder("insert into [t_category](CategoryId,[CategoryName],[ParentID],[Path],[OrderID],[Remark])");
            sql.Append(" values(@categoryId,@name,@parentId,@path,@orderId,@remark);");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction tran = connection.BeginTransaction();

                try
                {
                    System.Data.Common.DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
                    db.AddInParameter(cmd, "categoryid", DbType.String, categoryEntity.CategoryId);
                    db.AddInParameter(cmd, "name", DbType.String, categoryEntity.CategoryName);
                    db.AddInParameter(cmd, "parentId", DbType.Int32, categoryEntity.ParentID);
                    db.AddInParameter(cmd, "path", DbType.String, categoryEntity.Path);
                    db.AddInParameter(cmd, "orderId", DbType.Int32, categoryEntity.OrderID);
                    db.AddInParameter(cmd, "remark", DbType.String, categoryEntity.Remark);

                    int result = db.ExecuteNonQuery(cmd, tran);
                    tran.Commit();

                    //清除缓存
                    _cacheManager.Remove(CacheKey);

                    return result;
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw new DalException("系统异常！", e);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public int Delete(Model.CategoryEntity categoryEntity)
        {
            const string sql = "Delete from [t_category] where ID = @id";

            try
            {
                System.Data.Common.DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "id", DbType.String, categoryEntity.CategoryName);

                //清除缓存
                _cacheManager.Remove(CacheKey);
                _cacheManager.Remove(CacheKey + "_" + categoryEntity.ParentID);

                int result = db.ExecuteNonQuery(cmd);
                return result;
            }
            catch (Exception e)
            {
                throw new DalException("系统异常！", e);
            }
        }

        /// <summary>
        /// 更新分类
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public int Update(Model.CategoryEntity categoryEntity)
        {
            var sql = new StringBuilder("update [t_category] set ");
            sql.Append("[CategoryName] = @name,");
            sql.Append("[ParentID] = @parentId,");
            sql.Append("[Path] = @path");
            sql.Append("[OrderID] = @orderId");
            sql.Append("[Remark] = @remark");
            sql.Append(" where ID = @id");
            try
            {
                System.Data.Common.DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
                db.AddInParameter(cmd, "name", DbType.String, categoryEntity.CategoryName);
                db.AddInParameter(cmd, "parentId", DbType.Int32, categoryEntity.ParentID);
                db.AddInParameter(cmd, "path", DbType.String, categoryEntity.Path);
                db.AddInParameter(cmd, "orderId", DbType.Int32, categoryEntity.OrderID);
                db.AddInParameter(cmd, "remark", DbType.String, categoryEntity.Remark);
                db.AddInParameter(cmd, "id", DbType.Int32, categoryEntity.ID);

                //清除缓存
                _cacheManager.Remove(CacheKey);
                _cacheManager.Remove(CacheKey + "_" + categoryEntity.ParentID);

                int result = db.ExecuteNonQuery(cmd);
                return result;

            }
            catch (Exception e)
            {
                throw new DalException("系统异常！", e);
            }
        }

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public List<Model.CategoryEntity> GetAll(Dictionary<string,string> orderDic)
        {
            List<Model.CategoryEntity> categoryEntities = new List<CategoryEntity>();

            try
            {
                if(_cacheManager.GetData(CacheKey) != null)
                {
                    return (List<Model.CategoryEntity>) _cacheManager.GetData(CacheKey);
                }

                string orderString = DalBase.ParseOrderCondition(orderDic);
                StringBuilder sql = new StringBuilder("SELECT [ID],[CategoryName],[ParentID],[Path],[OrderID],[Remark] FROM [t_category]");
                if (!string.IsNullOrEmpty(orderString))
                {
                    sql.Append(DalBase.ParseOrderCondition(orderDic));
                    sql.Append(",order by path,orderid asc");
                }
                else
                {
                    sql.Append(" order by path,orderid asc");
                }

                using (System.Data.IDataReader reader = 
                    db.ExecuteReader(System.Data.CommandType.Text, sql.ToString()))
                {
                    while (reader.Read())
                    {
                        categoryEntities.Add(new CategoryEntity
                        {
                            ID = Convert.ToInt32(reader[0]),
                            CategoryName = reader[1].ToString(),
                            ParentID = Convert.ToInt32(reader[2]),
                            Path = reader[3].ToString(),
                            OrderID = Convert.ToInt32(reader[4]),
                            Remark = reader[5].ToString()
                        });
                    }
                    reader.Close();
                    reader.Dispose();
                }

                _cacheManager.Add(CacheKey, categoryEntities);
            }
            catch (Exception e)
            {
                throw new DalException("系统异常！", e);
            }

            return categoryEntities;
        }

        /// <summary>
        /// 获取子类
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public IList<Model.CategoryEntity> GetByParentId(Model.CategoryEntity categoryEntity, Dictionary<string, string> orderDictionary)
        {
            StringBuilder sql = new StringBuilder("SELECT [ID],[CategoryName],[ParentID],[Path],[OrderID],[Remark] FROM [t_category] where ParentID=@parentId");
            sql.Append(DalBase.ParseOrderCondition(orderDictionary));
            IList<CategoryEntity> categoryEntities = new List<CategoryEntity>();

            try
            {
                DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
                db.AddInParameter(cmd, "parentId", DbType.Int32, categoryEntity.ParentID);

                using (System.Data.IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        categoryEntities.Add(new CategoryEntity
                        {
                            ID = Convert.ToInt32(reader[0]),
                            CategoryName = reader[1].ToString(),
                            ParentID = Convert.ToInt32(reader[2]),
                            Path = reader[3].ToString(),
                            OrderID = Convert.ToInt32(reader[4])
                        });
                    }
                    reader.Close();
                    reader.Dispose();
                }

                _cacheManager.Add(CacheKey + "_" + categoryEntity.ParentID, categoryEntities);
            }
            catch (Exception e)
            {
                throw new DalException("系统异常！", e);
            }

            return categoryEntities;
        }

        public Model.CategoryEntity GetById(int id)
        {
            StringBuilder sql = new StringBuilder("SELECT [ID],[CategoryName],[ParentID],[Path],[OrderID],[Remark] FROM [t_category] where ID=@id");
            CategoryEntity category = new CategoryEntity();

            try
            {
                DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
                db.AddInParameter(cmd, "id", DbType.Int32, id);

                using (System.Data.IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        category = new CategoryEntity
                        {
                            ID = Convert.ToInt32(reader[0]),
                            CategoryName = reader[1].ToString(),
                            ParentID = Convert.ToInt32(reader[2]),
                            Path = reader[3].ToString(),
                            OrderID = Convert.ToInt32(reader[4])
                        };
                    }
                    reader.Close();
                    reader.Dispose();
                }
            }
            catch (Exception e)
            {
                throw new DalException("系统异常！", e);
            }

            return category;
        }

    }
}
