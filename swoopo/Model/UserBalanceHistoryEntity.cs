using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.Model
{
    [Serializable]
    public class UserBalanceHistoryEntity
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public decimal Balance { get; set; }

        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
