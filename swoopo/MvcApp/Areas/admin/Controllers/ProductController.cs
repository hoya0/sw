using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Swoopo.Model;
using Swoopo.BLL;
using Swoopo.ExceptionHandler;
using System.Runtime.Serialization;

namespace MvcApp.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(PagerCondition condition)
        {
            if (condition == null)
            {
                condition = new PagerCondition()
                {
                    PageSize = 10,
                    PageIndex = 1
                };
            }
            Swoopo.Model.JsonResult json = new Swoopo.Model.JsonResult();
            try
            {
                List<ProductEntity> list = new ProductBLL().getPaged(condition);
                json.value = list;
            }
            catch (BllException e)
            {
#if DEBUG
                json.error = e.InnerException.Message;
#else
                json.error = e.Message;
#endif
            }
            catch (DalException ex)
            {
#if DEBUG
                json.error = ex.InnerException.Message;
#else
                json.error = ex.Message;
#endif
            }
            
            return Json(json);
        }

        public ActionResult Edit()
        {
            getCategories();
            return View("edit");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(ProductEntity product)
        {
            Swoopo.Model.JsonResult s = new Swoopo.Model.JsonResult();
            try
            {
                product.RemainTime =  product.EndTime.ToFileTime() - product.StarTime.ToFileTime();

                ProductBLL proBll = new ProductBLL();
                int result = proBll.Save(product);
                if (result > 0)
                {
                    s.value = 1;
                }
                else
                {
                    s.error = "保存失败！";
                }
            }
            catch (BllException e)
            { 
#if DEBUG
                s.error = e.InnerException.StackTrace;
#else
                s.error = e.Message;
#endif
            }
            catch (DalException ex)
            {
#if DEBUG
                s.error = ex.InnerException.StackTrace;
#else
                s.error = ex.Message;
#endif
            }
            return Json(s);
        }

        private void getCategories()
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

            ViewData["Cateories"] = new SelectList(items, "ID", "CategoryName");

            /*
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
            */
        }

    }//end class
}//end namespace
