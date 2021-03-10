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

            Assert.AreEqual(expectedCode, ((OkObjectResult)resp).StatusCode);
            Assert.AreEqual(expectedValue, ((OkObjectResult)resp).Value);

        }

        [TestCase("1 D 2", 400, "Invalid operator token.")]
        public void GetError(string calculation, int expectedCode, string expectedValue)
        {
            var sut = new CalcEngineController();
            var resp = sut.Get(calculation);

            Assert.AreEqual(expectedCode, ((BadRequestObjectResult)resp).StatusCode);
            Assert.AreEqual(expectedValue, ((BadRequestObjectResult)resp).Value);

        }

        [TestCase("1 / 3", 2, 200, 0.33)]
        [TestCase("2 / -3", 4, 200, -0.6667)]
        [TestCase("1/6", null, 200, 0.17)]
        public void GetSuccessPrecsision(string calculation, int? precision, int expectedCode, double expectedValue)
        {
            ActionResult resp = null;
            var sut = new CalcEngineController();
            if (precision.HasValue)
            {
                resp = sut.Get(calculation, precision);
            }
            else
            {
                resp = sut.Get(calculation);
            }

            Assert.AreEqual(expectedCode, ((OkObjectResult)resp).StatusCode);
            Assert.AreEqual(expectedValue, ((OkObjectResult)resp).Value);

        }
    }
}