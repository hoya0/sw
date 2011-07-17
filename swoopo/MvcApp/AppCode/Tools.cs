using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp.AppCode
{
    public static class Tools
    {
        public static string GenericCategoryId()
        {
            return DateTime.Now.Ticks.ToString();
        }

        public static string DeepBlank(string str, char[] splitChar)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            if (str.Split(splitChar).Length > 0)
            {
                string blank = "└";
                int len = str.Split(splitChar).Length;
                for (int i = 0; i < len - 1; i++)
                {
                    blank += "-";
                }
                return blank;
            }
            return string.Empty;
        }

    }//end class
}//end namespace