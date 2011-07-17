using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.ExceptionHandler
{
    public class DalException : Exception
    {
        public DalException(string message)
            : base(message)
        { }

        public DalException(string message, Exception ex)
            : base(message, ex)
        { }

    }//end class
}
