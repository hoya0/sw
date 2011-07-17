using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.Model
{
    [Serializable]
    public class UserPriceProductEntity
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int ProductID { get; set; }

        public decimal Price { get; set; }

        public DateTime PriceDate { get; set; }
    }
}
