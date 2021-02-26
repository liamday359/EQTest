using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalcEngine.Engine;
using System.Web;

namespace CalcEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcEngineController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get(string expr)
        {
            try
            {
                var parser = new Parser();
                var calculator = new CalculatorRPN();
                var retValue = Calculate(expr, parser, calculator);

                return Ok(retValue);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private double Calculate(string calculation, IParser parser, ICalculator calculator)
        {
            ArrayList tokens = parser.Parse(calculation);
            foreach (var token in tokens)
            {
                if (token.GetType() == typeof(double))
                {
                    calculator.AddNumber((double)token);
                }
                else if (token.GetType() == typeof(CalcOperator))
                {
                    calculator.AddOperator((CalcOperator)token);
                }
                else
                {
                    throw new Exception("Unknown token type.");
                }
            }

            calculator.FlushStack();
            return calculator.Value;

        }
    }
}
