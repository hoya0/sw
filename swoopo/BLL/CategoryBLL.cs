using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Swoopo.Model;
using Swoopo.DAL;
using Swoopo.ExceptionHandler;

namespace Swoopo.BLL
{
    public class CategoryBll
    {
        /// <summary>
        /// 获取分类的dictionary列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, CategoryEntity> GetDic()
        {
            try
            {
                var dal = new CategoryDAL();

                IList<CategoryEntity> categories = dal.GetAll(null);

                //定义字典类型，将List转换成字典类型，集合中的元素个数是相同的
                var dic = new Dictionary<int, CategoryEntity>(categories.Count);

                foreach (var category in categories)
                {
                    dic.Add(category.ID, category);
                }

                return dic;
            }
            catch (Exception ex)
            {
                throw new BllException("业务异常！", ex);
            }
        }

        /// <summary>
        /// 生成层级结构的分类目录
        /// </summary>
        /// <returns></returns>
        public List<CategoryEntity> GetAllToTree()
        {
            try
            {
                var dal = new Swoopo.DAL.CategoryDAL();

                List<CategoryEntity> categories = dal.GetAll(null);

                return categories;
            }
            catch(DalException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw new BllException("业务异常！", e);
            }
        }

        /// <summary>
        /// 通过parentid获取分类
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public IList<CategoryEntity> GetByParentId(CategoryEntity categoryEntity)
        {
            try
            {
                var categories = this.GetDic();

                return categories.Values.Where(category => categories.ContainsKey(category.ParentID)).ToList();
            }
            catch (Exception e)
            {
                throw new BllException("业务异常！", e);
            }
        }

        public CategoryEntity GetById(CategoryEntity category)
        {
            try
            {
                var categories = this.GetDic();

                return (Model.CategoryEntity)new CategoryDAL().GetById(category.ID);
            }
            catch (Exception e)
            {
                throw new BllException("业务异常！", e);
            }
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public bool Insert(CategoryEntity categoryEntity)
        {
            try
            {
                var dal = new CategoryDAL();
                return dal.Insert(categoryEntity) > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw new BllException("业务异常！", e);
            }
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public bool Delete(CategoryEntity categoryEntity)
        {
            ///涉及到级联删除，如果包含子类的话，则不删除
            try
            {
                int childs = new CategoryDAL().GetAll(null).Count(c => c.ParentID == categoryEntity.ParentID);
                if (childs > 0)
                {
                    throw new BllException("该分类含有子分类，请先删除子分类！");
                }

                var dal = new CategoryDAL();
                return dal.Delete(categoryEntity) > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw new BllException("业务异常！", e);
            }
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public bool Update(CategoryEntity categoryEntity)
        {
            try
            {
                var dal = new CategoryDAL();
                return dal.Update(categoryEntity) > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw new BllException("业务异常！", e);
            }
        }

    }
}