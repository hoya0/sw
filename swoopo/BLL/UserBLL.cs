using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Swoopo.DAL;
using Swoopo.Model;
using Swoopo.ExceptionHandler;

namespace Swoopo.BLL
{
    public class UserBll
    {
        public UserEntity Signin(UserEntity user)
        {
            Dictionary<string, string> condition = new Dictionary<string, string>();
            condition.Add("UserName", user.UserName);
            condition.Add("UserPwd", user.UserPwd);
            UserEntity userResult = new DAL.UserDAL().Signin(user, condition);

            if (userResult != null && userResult.UserState == 0)
            {
                throw new BllException("用户为停用状态，请联系管理员！");
            }

            return userResult;
        }
    }//end class
}
