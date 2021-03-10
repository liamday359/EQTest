using System;
using System.Collections;
using NUnit.Framework;
using LibCalcEngine;


namespace CalcEngineTest
{
    class ParserTests
    {
        [TestCase("3 + 2", 3, CalcOperatorType.Addition, 2)]
        [TestCase("3+2", 3, CalcOperatorType.Addition, 2)]
        [TestCase("3 / 2", 3, CalcOperatorType.Division, 2)]
        [TestCase("5.0 * 7", 5, CalcOperatorType.Multiplication, 7)]
        [TestCase("5.0 x -7", 5, CalcOperatorType.Multiplication, -7)]
        [TestCase("-1 * 0", -1, CalcOperatorType.Multiplication, 0)]
        [TestCase("-1 - -7", -1, CalcOperatorType.Subtraction, -7)]
        [TestCase("-1--7", -1, CalcOperatorType.Subtraction, -7)]
        public void TwoOperands(string parseString,  double op1, CalcOperatorType calcType, double op2)
        {
            var sut = new Parser();
            var tokenList = sut.Parse(parseString);

            Assert.AreEqual(3, tokenList.Count);
            Assert.AreEqual(op1, tokenList[0]);
            Assert.AreEqual(calcType, ((CalcOperator)tokenList[1]).OperatorType);
            Assert.AreEqual(op2, tokenList[2]);

        }

        [TestCase("2 + 3 - 4", 2, CalcOperatorType.Addition, 3, CalcOperatorType.Subtraction, 4)]
        [TestCase("-5.111 * 33 - 4", -5.111, CalcOperatorType.Multiplication, 33, CalcOperatorType.Subtraction, 4)]
        [TestCase("201 / 3 * 3", 201, CalcOperatorType.Division, 3, CalcOperatorType.Multiplication, 3)]
        public void ThreeOperands(string parseString, double op1, CalcOperatorType calcType1, double op2, CalcOperatorType calcType2, double op3)
        {
            var sut = new Parser();
            var tokenList = sut.Parse(parseString);

            Assert.AreEqual(5, tokenList.Count);
            Assert.AreEqual(op1, tokenList[0]);
            Assert.AreEqual(calcType1, ((CalcOperator)tokenList[1]).OperatorType);
            Assert.AreEqual(op2, tokenList[2]);
            Assert.AreEqual(calcType2, ((CalcOperator)tokenList[3]).OperatorType);
            Assert.AreEqual(op3, tokenList[4]);

        }

        [TestCase("2 D 3")]
        public void ParseException(string parseString)
        {
            var sut = new Parser();

            ArrayList x;
            var ex = Assert.Throws<Exception>(() => x = sut.Parse(parseString));
            Assert.AreEqual("Invalid operator token.", ex.Message);
        }

    }
}
