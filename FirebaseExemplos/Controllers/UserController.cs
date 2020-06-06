using System.Collections.Generic;
using System.Threading.Tasks;
using FireBaseExemplos.Models;
using FireBaseExemplos.Services;
using Microsoft.AspNetCore.Mvc;

namespace FireBaseExemplos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _fireBaseService;
        public UserController(UserService fireBaseService)
        {
            _fireBaseService = fireBaseService;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<UserFirebase>>> GetUsers([FromQuery] string name)
        {
            return Ok(await _fireBaseService.GetUsers(name));
        }

        [HttpGet("GetUsersStream")]
        public ActionResult<IEnumerable<UserFirebase>> GetUsersStream([FromQuery] string name)
        {
            return Ok(_fireBaseService.GetUsersStream(name));
        }

        [HttpGet("GetUsersById/{userId}")]
        public async Task<ActionResult<UserFirebase>> GetUsersById(string userId)
        {
            return Ok(await _fireBaseService.GetUsersById(userId));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<bool>> CreateUser([FromBody] UserFirebase user)
        {
            return Ok(await _fireBaseService.CreateUser(user));
        }

        [HttpPut("AlterUser")]
        public async Task<ActionResult<bool>> AlterUser([FromBody] UserFirebase user)
        {
            return Ok(await _fireBaseService.AlterUser(user));
        }

        [HttpPatch("AlterStatus/{userId}")]
        public async Task<ActionResult<bool>> AlterUser(string userId, [FromQuery]string status)
        {
            return Ok(await _fireBaseService.AlterStatus(userId, status));
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<ActionResult<bool>> DeleteUser(string userId)
        {
            return Ok(await _fireBaseService.DeleteUser(userId));
        }
    }
}