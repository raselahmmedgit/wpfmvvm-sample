using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RnD.WPFMVVMSample.Lib
{
    public class ValidationErrorInfo
    {
        public bool IsWarning { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

        public override string ToString()
        {
            return (string.Format("{0}: {1}, {2}",
           IsWarning ? "Warning" : "Error",
           ErrorCode,
           ErrorMessage));
        }
    }
}
