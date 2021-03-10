using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibCalcEngine;
using System.Web;

namespace CalcEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcEngineController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get(string expr, int? precision = 2)
        {
            try
            {
                var parser = new Parser();
                var calculator = new CalculatorRPN();
                var retValue = Calculate(expr, parser, calculator, precision);

                return Ok(retValue);

            }
            catch (TokenException ex)
            {
                // If the expression is formed badly then we won't be able to recover
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                try
                {
                    var parser = new Parser();
                    var calculator = new CalculatorAPI();
                    // Try the calculation again by injecting a calculator object to this function that uses the third-party API
                    var retValue = Calculate(expr, parser, calculator, precision);

                    return Ok(retValue);
                }
                catch (Exception exInner)
                {
                    return BadRequest(exInner.Message);
                }
            }

        }

        private double Calculate(string calculation, IParser parser, ICalculator calculator, int? precision)
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

            return calculator.Value(precision);

        }
    }
}
