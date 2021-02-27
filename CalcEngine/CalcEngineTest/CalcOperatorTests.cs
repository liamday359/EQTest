using System;
using NUnit.Framework;
using LibCalcEngine;

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

        [TestCase("+", CalcOperatorType.Addition)]
        [TestCase("-", CalcOperatorType.Subtraction)]
        [TestCase("*", CalcOperatorType.Multiplication)]
        [TestCase("x", CalcOperatorType.Multiplication)]
        [TestCase("/", CalcOperatorType.Division)]
        public void ParsedOperator(string operatorString, CalcOperatorType op)
        {
            CalcOperator calcOp = new CalcOperator(operatorString);
            Assert.AreEqual(calcOp.OperatorType, op);
        }

        [TestCase("t")]
        [TestCase("10")]
        [TestCase("&")]
        public void ParseException(string parseString)
        {
            CalcOperator x;
            var ex = Assert.Throws<Exception>(() => x = new CalcOperator(parseString));
            Assert.AreEqual("Invalid operator token.", ex.Message);
        }

    }
}
