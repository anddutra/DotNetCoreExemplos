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
        //Chamada realizada através do endereço http://localhost:5000/api/Value
        public ActionResult<int> GetRandomValue()
        {
            int result = _valueServices.GetRandomValue();
            return Ok(result);
        }

        [HttpGet("NextValue")]
        //Chamada realizada através do endereço http://localhost:5000/api/Value/NextValue
        public ActionResult<int> GetNextValue()
        {
            return Ok(_valueServices.GetNextValue());
        }

        [HttpGet("OddOrEven")]
        //Chamada realizada através do endereço http://localhost:5000/api/Value/OddOrEven?value=10
        public ActionResult<string> GetOddOrEven(int value)
        {
            return Ok(_valueServices.GetOddOrEven(value));
        }

        [HttpGet("Sum3Values/{value1}")]
        //Chamada realizada através do endereço http://localhost:5000/api/Value/Sum3Values/10/?value2=20
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