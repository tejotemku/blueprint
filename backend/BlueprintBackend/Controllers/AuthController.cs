using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlueprintBackend.Models;
using BlueprintBackend.Interfaces;
using BlueprintBackend.Exceptions;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BlueprintBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDataBase _database;

        public AuthController(IDataBase database, IConfiguration config)
        {
            _config = config;
            _database = database;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDto>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);
            try
            {
                _database.InsertUser(request.Username, request.Email, passwordHash, passwordSalt);
                return Ok(new UserRegisterDto(request.Username, request.Email, passwordHash, passwordSalt));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            try
            {
                CheckAuthentity(request.Username, request.Password);
                return Ok(CreateToken(request.Username, _database.GetUserEmail(request.Username)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string CreateToken(string username, string email)
        {
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, username),
                 new Claim(ClaimTypes.Email, email)
             };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT_SECRET")));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private bool CheckAuthentity(string username, string password)
        {
            (string passwordHash, string passwordSalt) = _database.GetUserPaswordHashAndSalt(username);
            
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(passwordSalt)))
            {
                var computedPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                if (computedPassword != passwordHash) throw new BadRequestException("Wrong username or password");
                return true;
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

