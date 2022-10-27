using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlueprintBackend.Models;
using System.Security.Cryptography;
using BlueprintBackend.Interfaces;
using System.Text;

namespace BlueprintBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IDataBase _database;

        public AuthController(IDataBase database)
        {
            _database = database;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDto>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);
            try
            {
                _database.InsertUser(request.Username, request.Email, passwordHash, passwordSalt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new UserRegisterDto(request.Username, request.Email, passwordHash, passwordSalt));
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            if(!CheckAuthentity(request.Username, request.Password))
            {
                return BadRequest("Wrong username or password");
            }
            string token = "super tokenciak"; 
            return Ok(token);
        }



        private bool CheckAuthentity(string username, string password)
        {
            (string passwordHash, string passwordSalt) = _database.GetUserPaswordHashAndSalt(username);
            
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(passwordSalt)))
            {
                var computedPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return computedPassword == passwordHash;
            }

        }

        private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                hmac.Key = Encoding.UTF8.GetBytes(passwordSalt);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
//config.GetValue<string>("JWT_SECRET")

