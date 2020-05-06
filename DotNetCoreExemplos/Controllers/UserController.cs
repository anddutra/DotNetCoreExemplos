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

        [HttpPost("SaveUser")]
        //Chamada realizada através do endereço http://localhost:5000/api/User/SaveUser
        //{
        //    "name": "Andre",
        //    "lastName": "Dutra",
        //    "email": "email@mail.com"
        //}
        public ActionResult<bool> SaveUserFile([FromBody]User user)
        {
            return Ok(_userServices.SaveUserFile(user));
        }

        [HttpDelete("DeleteUser/{id}")]
        //Chamada realizada através do endereço http://localhost:5000/api/User/DeleteUser/1
        public ActionResult<bool> DeleteUserFile(int id)
        {
            return Ok(_userServices.DeleteUserFile(id));
        }

        [HttpGet("ReadUsers")]
        //Chamada realizada através do endereço http://localhost:5000/api/User/ReadUsers?name=Andre
        public ActionResult<string> ReadUsersFile(string name)
        {
            return Ok(_userServices.ReadUsersFile(name));
        }
    }
}