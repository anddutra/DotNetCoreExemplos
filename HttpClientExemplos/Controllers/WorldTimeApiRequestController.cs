using System.Threading.Tasks;
using HttpClientExemplos.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientExemplos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldTimeApiRequestController : ControllerBase
    {
        WorldTimeApiRequestService _apiRequestServices;
        public WorldTimeApiRequestController(WorldTimeApiRequestService apiRequestServices)
        {
            _apiRequestServices = apiRequestServices;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetApiWorldTime()
        {
            return Ok(await _apiRequestServices.GetApiWorldTime());
        }
    }
}