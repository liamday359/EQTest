using NUnit.Framework;
using CalcEngine.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ControllerTest
{
    public class Tests
    {

        [TestCase("1 + 2", 200, 3)]
        [TestCase("1.0 * 2.2", 200, 2.2)]
        [TestCase("4 * 3 + 2", 200, 14)]
        [TestCase("2 + 4 * 3", 200, 14)]
        public void GetSuccess(string calculation, int expectedCode, double expectedValue)
        {
            var sut = new CalcEngineController();
            var resp = sut.Get(calculation);

            Assert.AreEqual(((OkObjectResult)resp).StatusCode, expectedCode);
            Assert.AreEqual(((OkObjectResult)resp).Value, expectedValue);

        }

        [TestCase("1 D 2", 400, "Invalid operator token.")]
        public void GetError(string calculation, int expectedCode, string expectedValue)
        {
            var sut = new CalcEngineController();
            var resp = sut.Get(calculation);

            Assert.AreEqual(((BadRequestObjectResult)resp).StatusCode, expectedCode);
            Assert.AreEqual(((BadRequestObjectResult)resp).Value, expectedValue);

        }
    }
}