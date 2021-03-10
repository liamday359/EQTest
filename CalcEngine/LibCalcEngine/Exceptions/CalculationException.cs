using System;
using System.Collections.Generic;
using System.Text;

namespace LibCalcEngine
{
    [Serializable]
    public class CalculatorException : Exception
    {
        public CalculatorException()
        { }

        public CalculatorException(string message)
            : base(message)
        { }

        public CalculatorException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

}