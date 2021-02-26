using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibCalcEngine
{
    public enum CalcOperatorType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class CalcOperator
    {
        public CalcOperatorType OperatorType { get; }

        public CalcOperator(CalcOperatorType typeIn)
        {
            OperatorType = typeIn;
        }

        public CalcOperator(string token)
        {
            switch (token.Trim().ToUpper())
            {
                case "+":
                    OperatorType = CalcOperatorType.Addition;
                    break;

                case "-":
                    OperatorType = CalcOperatorType.Subtraction;
                    break;

                case "/":
                    OperatorType = CalcOperatorType.Division;
                    break;

                case "*":
                case "X":
                    OperatorType = CalcOperatorType.Multiplication;
                    break;

                default:
                    throw new Exception("Invalid operator token.");
            }
        }

        public short Precedence
        {
            get
            {
                switch (OperatorType)
                {
                    case CalcOperatorType.Addition:
                    case CalcOperatorType.Subtraction:
                        return 2;

                    case CalcOperatorType.Multiplication:
                    case CalcOperatorType.Division:
                        return 3;

                    default:
                        return 0;

                }
            }
        }

        public override string ToString()
        {
            switch (OperatorType)
            {
                case CalcOperatorType.Addition:
                    return "+";

                case CalcOperatorType.Subtraction:
                    return "-";

                case CalcOperatorType.Multiplication:
                    return "*";

                case CalcOperatorType.Division:
                    return "/";

                default:
                    return "";

            }

        }
    }
}
