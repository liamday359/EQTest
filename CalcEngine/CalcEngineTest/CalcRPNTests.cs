using NUnit.Framework;
using CalcEngine;
using CalcEngine.Engine;

namespace CalcEngineTest
{
    class CalcRPNTests
    {
        [TestCase(3, CalcOperatorType.Addition, 2, 5)] // 3 + 2
        [TestCase(5, CalcOperatorType.Subtraction, 10, -5)] // 5 - 10
        [TestCase(1, CalcOperatorType.Subtraction, -1, 2)] // 1 - -1
        [TestCase(3.2, CalcOperatorType.Multiplication, 20, 64)] // 3.2 * 20
        [TestCase(6, CalcOperatorType.Division, 0.5, 12)] // 6 / 0.5
        public void TwoOperands(double op1, CalcOperatorType calcType, double op2, double result)
        {
            var sut = new CalculatorRPN();
            sut.AddNumber(op1);
            sut.AddOperator(new CalcOperator(calcType));
            sut.AddNumber(op2);
            sut.FlushStack();

            Assert.AreEqual(sut.Value, result);

        }

        [TestCase(3, CalcOperatorType.Addition, 2, CalcOperatorType.Addition, 7, 12)] // 3 + 2 + 7
        [TestCase(5, CalcOperatorType.Addition, 1, CalcOperatorType.Multiplication, 4, 9)] // 5 + 1 * 4
        [TestCase(1, CalcOperatorType.Multiplication, 4, CalcOperatorType.Addition, 5, 9)] // 1 * 4 + 5
        [TestCase(10, CalcOperatorType.Subtraction, 4, CalcOperatorType.Division, 2, 8)] // 10 - 4 / 2
        public void ThreeOperands(double op1, CalcOperatorType calcType1, double op2, CalcOperatorType calcType2, double op3, double result)
        {
            var sut = new CalculatorRPN();
            sut.AddNumber(op1);
            sut.AddOperator(new CalcOperator(calcType1));
            sut.AddNumber(op2);
            sut.AddOperator(new CalcOperator(calcType2));
            sut.AddNumber(op3);
            sut.FlushStack();

            Assert.AreEqual(sut.Value, result);
        }

        [TestCase(3, CalcOperatorType.Addition)] // 3 +
        public void SingleOperandException(double op1, CalcOperatorType calcType)
        {
            var sut = new CalculatorRPN();
            sut.AddNumber(op1);
            sut.AddOperator(new CalcOperator(calcType));
            sut.FlushStack();

            double x;
            var ex = Assert.Throws<CalculatorException>(() => x = sut.Value);
            Assert.AreEqual("Not enough values to perform operation.", ex.Message);
        }

        [TestCase(3, CalcOperatorType.Addition, 2, 1)] // 3 + 2 1
        public void TooManyOperandException(double op1, CalcOperatorType calcType, double op2, double op3)
        {
            var sut = new CalculatorRPN();
            sut.AddNumber(op1);
            sut.AddOperator(new CalcOperator(calcType));
            sut.AddNumber(op2);
            sut.AddNumber(op3);
            sut.FlushStack();

            double x;
            var ex = Assert.Throws<CalculatorException>(() => x = sut.Value);
            Assert.AreEqual("Too many remaining operands.", ex.Message);
        }

    }
}

