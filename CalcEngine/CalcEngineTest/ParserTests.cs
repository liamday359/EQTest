using NUnit.Framework;
using CalcEngine;
using CalcEngine.Engine;


namespace CalcEngineTest
{
    class ParserTests
    {
        [TestCase("3 + 2", 3, CalcOperatorType.Addition, 2)]
        [TestCase("3 / 2", 3, CalcOperatorType.Division, 2)]
        [TestCase("5.0 * 7", 5, CalcOperatorType.Multiplication, 7)]
        [TestCase("5.0 x -7", 5, CalcOperatorType.Multiplication, -7)]
        [TestCase("-1 * 0", -1, CalcOperatorType.Multiplication, 0)]
        [TestCase("-1 - -7", -1, CalcOperatorType.Subtraction, -7)]
        public void TwoOperands(string parseString,  double op1, CalcOperatorType calcType, double op2)
        {
            var sut = new Parser();
            var tokenList = sut.Parse(parseString);

            Assert.AreEqual(tokenList.Count, 3);
            Assert.AreEqual(tokenList[0], op1);
            Assert.AreEqual(((CalcOperator)tokenList[1]).OperatorType, calcType);
            Assert.AreEqual(tokenList[2], op2);

        }

        [TestCase("2 + 3 - 4", 2, CalcOperatorType.Addition, 3, CalcOperatorType.Subtraction, 4)]
        [TestCase("-5.111 * 33 - 4", -5.111, CalcOperatorType.Multiplication, 33, CalcOperatorType.Subtraction, 4)]
        [TestCase("201 / 3 * 3", 201, CalcOperatorType.Division, 3, CalcOperatorType.Multiplication, 3)]
        public void ThreeOperands(string parseString, double op1, CalcOperatorType calcType1, double op2, CalcOperatorType calcType2, double op3)
        {
            var sut = new Parser();
            var tokenList = sut.Parse(parseString);

            Assert.AreEqual(tokenList.Count, 5);
            Assert.AreEqual(tokenList[0], op1);
            Assert.AreEqual(((CalcOperator)tokenList[1]).OperatorType, calcType1);
            Assert.AreEqual(tokenList[2], op2);
            Assert.AreEqual(((CalcOperator)tokenList[3]).OperatorType, calcType2);
            Assert.AreEqual(tokenList[4], op3);

        }

    }
}
