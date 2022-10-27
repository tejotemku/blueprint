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



        private bool CheckAuthentity(string username, string password)
        {

        }

        private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }

            Console.WriteLine(passwordSalt);
            Console.WriteLine(passwordHash);
        }
    }
}
//config.GetValue<string>("JWT_SECRET")

