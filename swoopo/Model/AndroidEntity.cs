using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.Model
{
    [Serializable]
    public class AndroidEntity
    {
        public int ID { get; set; }
        public string UserName { get; set; }

        private byte _state = 1;
        public Byte State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
    }
}
