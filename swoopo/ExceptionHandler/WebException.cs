using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swoopo.ExceptionHandler
{
    public class WebException : Exception
    {
        public WebException(string message)
            : base(message)
        { }

        public WebException(string message, Exception ex)
            : base(message, ex)
        { }

    }//end class
}
