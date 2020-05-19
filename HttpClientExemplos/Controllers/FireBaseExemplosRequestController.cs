using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientExemplos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientExemplos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FireBaseExemplosRequestController : ControllerBase
    {
        FireBaseExemplosRequestService _fireBaseRequestService;
        public FireBaseExemplosRequestController(FireBaseExemplosRequestService fireBaseRequestService)
        {
            _fireBaseRequestService = fireBaseRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetApiFireBaseExemplos()
        {
            return Ok(await _fireBaseRequestService.GetApiFireBaseExemplos());
        }
    }
}