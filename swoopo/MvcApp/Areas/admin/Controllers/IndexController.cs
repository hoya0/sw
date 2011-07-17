using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Swoopo.Model;
using Swoopo.BLL;
using Swoopo.Utils;
using Swoopo.ExceptionHandler;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace MvcApp.Areas.Admin.Controllers
{
    [HandleError]
    public class IndexController : Controller
    {

        #region 首页

        public ActionResult Index()
        {
            if (Session["UserEntity"] == null) {
                return Redirect("~/Admin/Signin");
            }
            return View();
        }

        #endregion

    }//end class
}
