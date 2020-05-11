﻿using System.Threading.Tasks;
using DotNetCoreExemplos.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreExemplos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRequestController : ControllerBase
    {
        ApiRequestServices _apiRequestServices;
        public ApiRequestController(ApiRequestServices apiRequestServices)
        {
            _apiRequestServices = apiRequestServices;
        }

        [HttpGet("ApiWorldTime")]
        //Chamada realizada através do endereço http://localhost:5000/api/ApiRequest/ApiWorldTime
        public async Task<ActionResult<string>> GetApiWorldTimeAsync()
        {
            return Ok(await _apiRequestServices.GetApiWorldTime());
        }
    }
}