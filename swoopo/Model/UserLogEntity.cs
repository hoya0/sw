using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.Model
{
    [Serializable]
    public class UserLogEntity
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public DateTime LoginTime { get; set; }

        public string LoginIP { get; set; }
    }
}
