using NUnit.Framework;
using CalcEngine;
using CalcEngine.Engine;

namespace CalcEngineTest
{
    class CalcOperatorTests
    {

        [TestCase(CalcOperatorType.Addition, CalcOperatorType.Addition)]
        [TestCase(CalcOperatorType.Addition, CalcOperatorType.Subtraction)]
        [TestCase(CalcOperatorType.Subtraction, CalcOperatorType.Subtraction)]
        [TestCase(CalcOperatorType.Multiplication, CalcOperatorType.Multiplication)]
        [TestCase(CalcOperatorType.Multiplication, CalcOperatorType.Division)]
        [TestCase(CalcOperatorType.Division, CalcOperatorType.Division)]
        public void EqualPrecedence(CalcOperatorType op1, CalcOperatorType op2)
        {
            CalcOperator calcop1 = new CalcOperator(op1);
            CalcOperator calcop2 = new CalcOperator(op2);
            Assert.AreEqual(calcop1.Precedence, calcop2.Precedence);
        }

        [TestCase(CalcOperatorType.Addition, CalcOperatorType.Multiplication)]
        [TestCase(CalcOperatorType.Subtraction, CalcOperatorType.Multiplication)]
        [TestCase(CalcOperatorType.Addition, CalcOperatorType.Division)]
        [TestCase(CalcOperatorType.Subtraction, CalcOperatorType.Division)]
        public void NonEqualPrecedence(CalcOperatorType op1, CalcOperatorType op2)
        {
            CalcOperator calcop1 = new CalcOperator(op1);
            CalcOperator calcop2 = new CalcOperator(op2);
            Assert.AreNotEqual(calcop1.Precedence, calcop2.Precedence);
        }

    }
}
