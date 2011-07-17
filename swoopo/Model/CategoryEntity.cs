using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.Model
{
    [Serializable]
    public class CategoryEntity
    {
        public int ID { get; set; }

        /// <summary>
        /// 逻辑Id
        /// </summary>
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int ParentID { get; set; }

        public string Path { get; set; }

        public int OrderID { get; set; }

        public string Remark { get; set; }

        public IList<CategoryEntity> children;

        public object Clone(bool isDeep)
        {
            if (isDeep == true)
            {
                return new CategoryEntity() as object;
            }
            else
            {
                return this.MemberwiseClone();
            }
        }

    }
}
