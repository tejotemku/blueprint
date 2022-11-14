using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlueprintBackend.Models;
using BlueprintBackend.Interfaces;
using BlueprintBackend.Exceptions;
using BlueprintBackend.Utils;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using Npgsql;
using System.Net;

namespace BlueprintBackend.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IDataBase _database;
    private readonly BlueprintUtils _utils;

    public AuthController(IDataBase database, BlueprintUtils utils)
    {
        _database = database;
        _utils = utils;
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(RegisterUserDto request)
    {
        CreatePasswordHash(request.password, out string passwordHash, out string passwordSalt);
        try
        {
            _database.InsertUser(request.username, request.email, passwordHash, passwordSalt);
            return Ok(CreateToken(request.username, request.email));
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case SocketException:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                case NpgsqlException:
                    return StatusCode((int)HttpStatusCode.Conflict);
                default:
                    return BadRequest(ex.Message);
            }
        }
    }


    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginUser request)
    {
        try
        {
            CheckAuthentity(request.username, request.password);
            return Ok(CreateToken(request.username, _database.GetUserEmail(request.username)));
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case SocketException:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                case LoginFailed:
                    return StatusCode((int)HttpStatusCode.Unauthorized);
                default:
                    return BadRequest(ex.Message);
            }
        }
    }


    [HttpGet("check-and-refresh-token"), Authorize]
    public async Task<ActionResult<string>> CheckAndRefreshToken()
    {
        try
        {
            _utils.AuthenticateToken(
                Request.Headers.Authorization,
                out string username,
                out string email);
            return Ok(CreateToken(username, email));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("me"), Authorize]
    public async Task<ActionResult<MeDto>> GetMe()
    {
        try
        {
            _utils.AuthenticateToken(
                Request.Headers.Authorization,
                out string username,
                out string email);
            return Ok(new MeDto(username));
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
            Encoding.UTF8.GetBytes(_utils.GetJWTSecret()));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    private bool CheckAuthentity(string username, string password)
    {
        _database.GetUserPaswordHashAndSalt(username, out string passwordHash, out string passwordSalt);
        
        using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(passwordSalt)))
        {
            var computedPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            if (computedPassword != passwordHash) throw new LoginFailed("Wrong username or password");
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

