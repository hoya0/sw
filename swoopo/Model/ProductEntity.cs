using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.Model
{
    [Serializable]
    public class ProductEntity
    {
        public int ID { get; set; }

        public string ProName { get; set; }

        public DateTime StarTime { get; set; }

        public DateTime EndTime { get; set; }

        public byte ProState { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 秒杀剩余时间
        /// </summary>
        public long RemainTime { get; set; }

        public DateTime CreateTime { get; set; }

        public int CategoryID { get; set; }
    }
}
