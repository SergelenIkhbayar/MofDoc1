using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MofDoc.Class
{
    public class MofException : Exception
    {
        public int errorCode = 0;

        public MofException(string Message)
            : base(Message, new Exception(Message))
        {
        }

        public MofException(string Message, Exception Inner)
            : base(Message, Inner)
        {
        }

        public MofException(int errorCode, string Message, Exception Inner)
            : base(Message, Inner)
        {
            this.errorCode = errorCode;
        }
    }
}