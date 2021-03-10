using System;
using System.Collections.Generic;
using System.Text;

namespace LibCalcEngine
{
    [Serializable]
    public class TokenException : Exception
    {
        public TokenException()
        { }

        public TokenException(string message)
            : base(message)
        { }

        public TokenException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

}
