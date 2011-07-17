using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Swoopo.Model;
using Swoopo.BLL;
using Swoopo.Utils;
using Swoopo.ExceptionHandler;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace MvcApp.Areas.Admin.Controllers
{
    public class SigninController : Controller
    {
        //
        // GET: /Admin/Signin/

        public ActionResult Index()
        {
            return View();
        }

        #region 登录

        [HttpPost]
        public ActionResult Signin(FormCollection form)
        {
            var returnResult = new Swoopo.Model.JsonResult();
            UserEntity userResult;
            try
            {
                var bll = new UserBll();
                var user = new UserEntity
                {
                    UserName = form.Get("UserName"),
                    UserPwd = form.Get("UserPwd"),
                    Email = "test@swoopo.com"
                };
                //由于登录时，不需要email，所有用“test@swoopo.com”替代，否则验证无法通过

                Validator<UserEntity> userValid = ValidationFactory.CreateValidator<UserEntity>();
                ValidationResults r = userValid.Validate(user);
                if (!r.IsValid)
                {
                    var list = r.Select(result => result.Message).ToList();

                    returnResult.error = list;
                    return Json(returnResult);
                }
                user.UserPwd = We7Utils.MD5(user.UserPwd);
                userResult = bll.Signin(user);
            }
            catch (BllException ex)
            {
                #if DEBUG
                returnResult.error = ex.InnerException.Message;
                #else
                returnResult.error = ex.Message;
                #endif
                return Json(returnResult);
            }
            catch (DalException dalException)
            {
                #if DEBUG
                returnResult.error = dalException.InnerException.Message;
                #else
                returnResult.error = dalException.Message;
                #endif
                return Json(returnResult);
            }

            if (userResult == null)
            {
                //ModelState.AddModelError("", "用户名或密码不正确!");
                //return View("Signin");
                returnResult.error = "用户名或密码不正确!";
                return Json(returnResult);
            }
            returnResult.value = true;
            Session["UserEntity"] = userResult;

            return Json(returnResult);
        }

        #endregion

    }
}
