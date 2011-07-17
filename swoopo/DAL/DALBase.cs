using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Swoopo.DAL
{
    public class DalBase
    {
        protected static Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("DatabaseConnectionString");

        protected static string ParseCondition(Dictionary<string, string> conditionDic)
        {
            if (conditionDic == null)
            {
                return string.Empty;
            }
            StringBuilder str = new StringBuilder();
            int i = 0, len = conditionDic.Keys.Count - 1;
            foreach (KeyValuePair<string, string> item in conditionDic)
            {
                if (i == len)
                {
                    str.Append(item.Key).Append("='").Append(item.Value).Append("'");
                }
                else
                {
                    str.Append(item.Key).Append("='").Append(item.Value).Append("' and ");
                }
                i++;
            }
            return str.ToString();
        }

        protected static string ParseOrderCondition(Dictionary<string,string> orderDictionary)
        {
            if (orderDictionary == null)
            {
                return string.Empty;
            }
            StringBuilder str = new StringBuilder();
            int i = 0, len = orderDictionary.Keys.Count - 1;
            if (len > 0)
            {
                str.Append(" Order By ");
            }
            foreach (KeyValuePair<string, string> item in orderDictionary)
            {
                if (i == len)
                {
                    str.Append(item.Key).Append(" ").Append(item.Value);
                }
                else
                {
                    str.Append(item.Key).Append(" ").Append(item.Value).Append(",");
                }
                i++;
            }
            return str.ToString();
        }

    }
}
