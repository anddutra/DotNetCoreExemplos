using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreExemplos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreExemplos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        CryptoRijndaelService _cryptoRijndaelService;
        public CryptoController(CryptoRijndaelService cryptoRijndaelService)
        {
            _cryptoRijndaelService = cryptoRijndaelService;
        }

        [HttpGet("GetEncryptPassword/{password}")]
        public ActionResult<string> GetEncryptPassword(string password)
        {
            return Ok(_cryptoRijndaelService.GetEncryptPassword(password));
        }

        [HttpGet("GetDecryptPassword/{encryptPassword}")]
        public ActionResult<string> GetDecryptPassword(string encryptPassword)
        {
            return Ok(_cryptoRijndaelService.GetDecryptPassword(encryptPassword));
        }
    }
}