using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalcEngine.Engine
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

    public interface ICalculator
    {
        void AddNumber(double numberIn);
        void AddOperator(CalcOperator operatorIn);
        void FlushStack();
        double Value { get; }
    }

    public class CalculatorRPN : ICalculator
    {
        private Stack<CalcOperator> RPNOperatorStack = new Stack<CalcOperator>();
        private ArrayList RPNList = new ArrayList();
        private Stack<double> TokenStack = new Stack<double>();

        // This is an implemention of the Shunting-yard algorithm https://en.wikipedia.org/wiki/Shunting-yard_algorithm 
        // Pushes a list of Reverse Polish Notation tokens to RPNList
        public void AddNumber(double numberIn)
        {
            RPNList.Add(numberIn);
        }

        public void AddOperator(CalcOperator operatorIn)
        {
            if (RPNOperatorStack.Count == 0)
            {
                RPNOperatorStack.Push(operatorIn);
            }
            else if (RPNOperatorStack.Peek().Precedence >= operatorIn.Precedence)
            {
                RPNList.Add(RPNOperatorStack.Pop());
                // Keep going until stack if fully popped or operator with lower precedence is on the stack
                AddOperator(operatorIn);

            }
            else if (RPNOperatorStack.Peek().Precedence < operatorIn.Precedence)
            {
                RPNOperatorStack.Push(operatorIn);
            }
        }

        public void FlushStack()
        {
            while (RPNOperatorStack.Count > 0)
            {
                RPNList.Add(RPNOperatorStack.Pop());
            }
        }



        // Evaluates a list of Reverse Polish Notation tokens. See https://en.wikipedia.org/wiki/Reverse_Polish_notation
        public double Value
        {
            get
            {
                foreach (object RPNLoop in RPNList)
                {
                    if (RPNLoop.GetType() == typeof(double))
                    {
                        TokenStack.Push((double)RPNLoop);
                    }
                    else if (RPNLoop.GetType() == typeof(CalcOperator))
                    {
                        var op = (CalcOperator)RPNLoop;

                        if (TokenStack.Count < 2)
                        {
                            throw new CalculatorException("Not enough values to perform operation.");
                        }

                        var oparand2 = TokenStack.Pop();
                        var operand1 = TokenStack.Pop();

                        switch (op.OperatorType)
                        {
                            case CalcOperatorType.Addition:
                                TokenStack.Push(operand1 + oparand2);
                                break;

                            case CalcOperatorType.Subtraction:
                                TokenStack.Push(operand1 - oparand2);
                                break;

                            case CalcOperatorType.Multiplication:
                                TokenStack.Push(operand1 * oparand2);
                                break;

                            case CalcOperatorType.Division:
                                TokenStack.Push(operand1 / oparand2);
                                break;

                        }
                    }
                    else
                    {
                        throw new CalculatorException("Unknown type in list.");
                    }
                }

                if (TokenStack.Count() > 1)
                {
                    throw new CalculatorException("Too many remaining operands.");
                }

                return TokenStack.Pop();
            }


        }
    }
}
