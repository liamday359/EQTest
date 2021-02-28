using System;
using System.Collections.Generic;
using System.Text;

namespace LibCalcEngine
{    public interface ICalculator
    {
        void AddNumber(double numberIn);
        void AddOperator(CalcOperator operatorIn);
        double Value { get; }
    }

}
