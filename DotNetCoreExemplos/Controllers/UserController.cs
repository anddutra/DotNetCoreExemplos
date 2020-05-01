using DotNetCoreExemplos.Models;
using DotNetCoreExemplos.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreExemplos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserServices _userServices;
        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("SaveUserFile")]
        //Chamada realizada através do endereço http://localhost:5000/api/User/SaveUserFile
        public ActionResult<bool> SaveUserFile([FromBody]User user)
        {
            return Ok(_userServices.SaveUserFile(user));
        }

        [HttpGet("ReadUserFile")]
        //Chamada realizada através do endereço http://localhost:5000/api/User/ReadUserFile
        public ActionResult<string> ReadUserFile()
        {
            return Ok(_userServices.ReadUserFile());
        }
    }
}