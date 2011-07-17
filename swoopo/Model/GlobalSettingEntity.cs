using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.Model
{
    [Serializable]
    public class GlobalSettingEntity
    {
        public int ID { get; set; }

        /// <summary>
        /// 每次出价金额
        /// </summary>
        public decimal PriceLimit { get; set; }
    }
}
