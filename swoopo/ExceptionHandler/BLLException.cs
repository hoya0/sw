using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swoopo.ExceptionHandler
{
    public class BllException : Exception
    {
        public BllException(string message)
            : base(message)
        { }

        public BllException(string message, Exception ex)
            : base(message, ex)
        { }

    }//end class
}
