using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_Dealer_Mobil_Acc.Models;
using API_Dealer_Mobil_Acc.Helper;
using API_Dealer_Mobil_Acc.Context;

namespace API_Dealer_Mobil_Acc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly string _constr;
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
            _constr = config.GetConnectionString("WebApiDatabase");
        }

        [HttpPost]
        public IActionResult LoginUser([FromBody] Login loginRequest)
        {
            var context = new LoginContext(_constr, _config);
            var listLogin = context.Autentifikasi(loginRequest.Username, loginRequest.Password);

            if (listLogin.Count == 0)
            {
                return Unauthorized(new { message = "Username atau password salah!" });
            }

            return Ok(listLogin);
        }
    }
}
