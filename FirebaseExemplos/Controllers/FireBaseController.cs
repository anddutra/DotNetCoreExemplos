using System.Collections.Generic;
using System.Threading.Tasks;
using FireBaseExemplos.Models;
using FireBaseExemplos.Services;
using Microsoft.AspNetCore.Mvc;

namespace FireBaseExemplos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FireBaseController : ControllerBase
    {
        private readonly FireBaseService _fireBaseService;
        public FireBaseController(FireBaseService fireBaseService)
        {
            _fireBaseService = fireBaseService;
        }

        [HttpGet("GetUsers")]
        //Chamada realizada através do endereço http://localhost:5000/api/FireBase/GetUsers
        public async Task<ActionResult<IEnumerable<UserFirebase>>> GetUsers([FromQuery] string name)
        {
            return Ok(await _fireBaseService.GetUsers(name));
        }

        [HttpGet("GetDocument/{id}")]
        //Chamada realizada através do endereço http://localhost:5000/api/FireBase/GetUser/id
        public async Task<ActionResult<UserFirebase>> GetDocument(string id)
        {
            return Ok(await _fireBaseService.GetDocument(id));
        }

        [HttpPost("CreateUser")]
        //Chamada realizada através do endereço http://localhost:5000/api/FireBase/CreateUser
        public async Task<ActionResult<bool>> CreateUser([FromBody] UserFirebase user)
        {
            return Ok(await _fireBaseService.CreateUser(user));
        }

        [HttpDelete("DeleteUser/{userId}")]
        //Chamada realizada através do endereço http://localhost:5000/api/FireBase/DeleteUser/id
        public async Task<ActionResult<bool>> DeleteUser(string userId)
        {
            return Ok(await _fireBaseService.DeleteUser(userId));
        }
    }
}