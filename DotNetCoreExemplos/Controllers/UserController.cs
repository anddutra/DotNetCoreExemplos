using DotNetCoreExemplos.Models;
using DotNetCoreExemplos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotNetCoreExemplos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userServices;
        public UserController(UserService userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("SaveUser")]
        public ActionResult<bool> SaveUserFile([FromBody]User user)
        {
            return Ok(_userServices.SaveUserFile(user));
        }

        [HttpDelete("DeleteUser/{id}")]
        public ActionResult<bool> DeleteUserFile(int id)
        {
            return Ok(_userServices.DeleteUserFile(id));
        }

        [HttpGet("ReadUsers")]
        public ActionResult<List<User>> ReadUsersFile(string name)
        {
            return Ok(_userServices.ReadUsersFile(name));
        }
    }
}