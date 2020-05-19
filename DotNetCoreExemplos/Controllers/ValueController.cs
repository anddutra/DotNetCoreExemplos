using DotNetCoreExemplos.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreExemplos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        ValueService _valueServices;
        public ValueController(ValueService valueServices)
        {
            _valueServices = valueServices;
        }

        [HttpGet]
        public ActionResult<int> GetRandomValue()
        {
            int result = _valueServices.GetRandomValue();
            return Ok(result);
        }

        [HttpGet("NextValue")]
        public ActionResult<int> GetNextValue()
        {
            return Ok(_valueServices.GetNextValue());
        }

        [HttpGet("OddOrEven")]
        public ActionResult<string> GetOddOrEven(int value)
        {
            return Ok(_valueServices.GetOddOrEven(value));
        }

        [HttpGet("Sum3Values/{value1}")]
        public ActionResult<int> GetSum3Values(int value1, [FromQuery]int value2, [FromHeader]int value3)
        {
            if (value3 == 0)
            {
                return BadRequest("Value3 informado no header da requisição deve ser maior que zero!");
            }

            int result = _valueServices.GetSum3Values(value1, value2, value3);
            return Ok(result);
        }
    }
}