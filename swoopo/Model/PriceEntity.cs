using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.Model
{
    [Serializable]
    public class PriceEntity
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        public decimal UserPrice { get; set; }

        public int UserID { get; set; }

        public DateTime PriceTime { get; set; }
    }
}
