using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Dtos;
using UsersApi.Models.Request;
using UsersApi.Utils;

namespace UsersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost("getToken")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginModel login)
        {
            var idToken = await FirebaseUtils.GetFirebaseIdToken(login);
            return Ok(idToken);
        }
    }
}
