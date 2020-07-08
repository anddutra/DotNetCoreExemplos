using DotNetCoreExemplos.Models;
using DotNetCoreExemplos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreExemplos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqController : ControllerBase
    {
        private readonly LinqService _linqService;
        public LinqController(LinqService linqService)
        {
            _linqService = linqService;
        }

        [HttpGet("GetSum")]
        public ActionResult<double> GetSum()
        {
            return Ok(_linqService.GetSum());
        }

        [HttpGet("GetWhere/{valor}")]
        public ActionResult<IEnumerable<LinqModel>> GetWhere(double valor)
        {
            return Ok(_linqService.GetWhere(valor));
        }

        [HttpGet("GetWhereSelect/{valor}")]
        public ActionResult<IEnumerable<int>> GetWhereSelect(double valor)
        {
            return Ok(_linqService.GetWhereSelect(valor));
        }

        [HttpGet("GetSelectDistinct")]
        public ActionResult<IEnumerable<string>> GetSelectDistinct()
        {
            return Ok(_linqService.GetSelectDistinct());
        }

        [HttpGet("GetMax")]
        public ActionResult<double> GetMax()
        {
            return Ok(_linqService.GetMax());
        }

        [HttpGet("GetMin")]
        public ActionResult<double> GetMin()
        {
            return Ok(_linqService.GetMin());
        }

        [HttpGet("GetUnion")]
        public ActionResult<IEnumerable<LinqModel>> GetUnion()
        {
            return Ok(_linqService.GetUnion());
        }

        [HttpGet("GetGroupBy")]
        public ActionResult<IEnumerable<string>> GetGroupBy()
        {
            return Ok(_linqService.GetGroupBy());
        }

        [HttpGet("GetGroupBy2")]
        public ActionResult<object> GetGroupBy2()
        {
            return Ok(_linqService.GetGroupBy2());
        }

        [HttpGet("GetLinqQuery/{valor}")]
        public ActionResult<IEnumerable<LinqModel>> GetLinqQuery(double valor)
        {
            return Ok(_linqService.GetLinqQuery(valor));
        }
    }
}