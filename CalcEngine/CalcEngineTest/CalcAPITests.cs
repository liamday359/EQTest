using NUnit.Framework;
using LibCalcEngine;

namespace CalcEngineTest
{
    class CalcAPITests
    {

        [TestCase(3, CalcOperatorType.Addition, 2, 5)] // 3 + 2
        [TestCase(5, CalcOperatorType.Subtraction, 10, -5)] // 5 - 10
        [TestCase(1, CalcOperatorType.Subtraction, -1, 2)] // 1 - -1
        [TestCase(3.2, CalcOperatorType.Multiplication, 20, 64)] // 3.2 * 20
        [TestCase(6, CalcOperatorType.Division, 0.5, 12)] // 6 / 0.5
        public void TwoOperandsAPI(double op1, CalcOperatorType calcType, double op2, double result)
        {
            var sut = new CalculatorAPI();
            sut.AddNumber(op1);
            sut.AddOperator(new CalcOperator(calcType));
            sut.AddNumber(op2);

            Assert.AreEqual(sut.Value(), result);

        }

        [TestCase(3, CalcOperatorType.Addition, 2, CalcOperatorType.Addition, 7, 12)] // 3 + 2 + 7
        [TestCase(1, CalcOperatorType.Multiplication, 4, CalcOperatorType.Addition, 5, 9)] // 1 * 4 + 5
        public void ThreeOperandsAPI(double op1, CalcOperatorType calcType1, double op2, CalcOperatorType calcType2, double op3, double result)
        {
            var sut = new CalculatorAPI();
            sut.AddNumber(op1);
            sut.AddOperator(new CalcOperator(calcType1));
            sut.AddNumber(op2);
            sut.AddOperator(new CalcOperator(calcType2));
            sut.AddNumber(op3);

            Assert.AreEqual(sut.Value(), result);
        }

        [TestCase(5, CalcOperatorType.Addition, 1, CalcOperatorType.Multiplication, 4, 9)] // 5 + 1 * 4
        [TestCase(10, CalcOperatorType.Subtraction, 4, CalcOperatorType.Division, 2, 8)] // 10 - 4 / 2
        public void ThreeOperandsException(double op1, CalcOperatorType calcType1, double op2, CalcOperatorType calcType2, double op3, double result)
        {
            var sut = new CalculatorAPI();
            sut.AddNumber(op1);
            sut.AddOperator(new CalcOperator(calcType1));
            sut.AddNumber(op2);

            var ex = Assert.Throws<CalculatorException>(() => sut.AddOperator(new CalcOperator(calcType2)));
            Assert.AreEqual("API does not support BEDMAS processing.", ex.Message);

        }


        [TestCase(3, CalcOperatorType.Addition)] // 3 +
        public void SingleOperandExceptionAPI(double op1, CalcOperatorType calcType)
        {
            var sut = new CalculatorAPI();
            sut.AddNumber(op1);
            sut.AddOperator(new CalcOperator(calcType));

            double x;
            var ex = Assert.Throws<System.AggregateException>(() => x = sut.Value());
            Assert.AreEqual("One or more errors occurred. (Response status code does not indicate success: 400 (Bad Request).)", ex.Message);
        }

        [TestCase(3, CalcOperatorType.Addition, 2, 1)] // 3 + 2 1
        public void TooManyOperandExceptionAPI(double op1, CalcOperatorType calcType, double op2, double op3)
        {
            var sut = new CalculatorAPI();
            sut.AddNumber(op1);
            sut.AddOperator(new CalcOperator(calcType));
            sut.AddNumber(op2);
            sut.AddNumber(op3);

            double x;
            var ex = Assert.Throws<System.AggregateException>(() => x = sut.Value());
            Assert.AreEqual("One or more errors occurred. (Response status code does not indicate success: 400 (Bad Request).)", ex.Message);
        }

        [TestCase(1, CalcOperatorType.Division, 3, 2, 0.33)] // 1 / 3
        [TestCase(1, CalcOperatorType.Division, 6, 5, 0.16667)] // 1 / 6
        [TestCase(-2, CalcOperatorType.Division, 3, 2, -0.67)] // 1 / 6
        public void Precision(double op1, CalcOperatorType calcType, double op2, int precision, double expected)
        {
            var sut = new CalculatorAPI();
            sut.AddNumber(op1);
            sut.AddOperator(new CalcOperator(calcType));
            sut.AddNumber(op2);

            Assert.AreEqual(expected, sut.Value(precision));

        }

    }
}
