using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace BLL
{
    public class Class1
    {
        public void fn()
        {
            //创建CacheManager            
            CacheManager cacheManager = (CacheManager)CacheFactory.GetCacheManager();  //添加缓存项            
            cacheManager.Add("MyDataReader", "123");  //获取缓存项            
            string str = (String)cacheManager.GetData("MyDataReader");  //打印            
            Console.WriteLine(str);
        }
    }
}
