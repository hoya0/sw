using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Swoopo.Model;
using Swoopo.BLL;

namespace MvcApp.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Admin/Category/

        #region 产品分类管理

        /// <summary>
        /// 产品分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CategoryEdit(int? id)
        {
            var categories = new Swoopo.BLL.CategoryBll().GetAllToTree();
            IList<CategoryEntity> items = new List<CategoryEntity>();
            CategoryEntity c;
            foreach (var item in categories)
            {
                c = item.Clone(false) as CategoryEntity;
                c.CategoryName = AppCode.Tools.DeepBlank(c.Path, new char[] { ',' }) + " " + c.CategoryName;
                items.Add(c);
            }
            items.Insert(0, new CategoryEntity()
                            {
                                ID = 0,
                                CategoryName = "做为父类"
                            });
            
            ViewData["Cateories"] = new SelectList(items, "ID", "CategoryName");
            CategoryEntity category = new CategoryEntity();
            if (id != null && id > 0)
            {
                category = new Swoopo.BLL.CategoryBll().GetById(
                    new CategoryEntity
                    {
                        ID = Convert.ToInt32(id)
                    });
                
                ViewData["Cateories"] = new SelectList(items, "ID", "CategoryName", category.ParentID);
            }
            ViewData["categoryItem"] = category;

            return View();
        }

        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.JsonResult CategoryLists()
        {
            List<CategoryEntity> list = new CategoryBll().GetAllToTree();

            Swoopo.Model.JsonResult json = new Swoopo.Model.JsonResult();
            json.value = list;

            return Json(json);
        }

        /// <summary>
        /// view页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CategoryList()
        {
            return View();
        }

        /// <summary>
        ///插入分类 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult CategoryAction(FormCollection form)
        {
            int parentId;
            string categoryName = string.IsNullOrEmpty(form["CategoryName"]) ? 
                string.Empty : form["CategoryName"].ToString();
            int orderId;
            string remark = form["CategoryName"].ToString();

            int.TryParse(string.IsNullOrEmpty(form["ParentID"]) ? 
                string.Empty : form["ParentID"].ToString(), out parentId);
            int.TryParse(string.IsNullOrEmpty(form["OrderID"]) ? 
                string.Empty : form["OrderID"].ToString(), out orderId);

            string parentPath = string.Empty;
            string categoryId = AppCode.Tools.GenericCategoryId();
            CategoryBll bll = new CategoryBll();
            var returnResult = new Swoopo.Model.JsonResult();
            try
            {
                if (parentId == 0)
                {
                    parentPath = categoryId;
                }
                else
                {
                    CategoryEntity parent = new CategoryEntity()
                    {
                        ID = parentId
                    };

                    parentPath = bll.GetById(parent).Path + "," + categoryId;
                }

                CategoryEntity c = new CategoryEntity();
                c.CategoryId = categoryId;
                c.CategoryName = categoryName;
                c.ParentID = parentId;
                c.Path = parentPath;
                c.OrderID = orderId;
                c.Remark = remark;

                returnResult.value = bll.Insert(c);
            }
            catch (Exception ex)
            {
                returnResult.error = ex.Message;
            }
            
            return Json(returnResult);
        }

        #endregion

    }
}