using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlueprintBackend.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BlueprintBackend.Utils;
public class BlueprintUtils
{
    readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private readonly IConfiguration _config;
    private readonly IDataBase _dataBase;

    public BlueprintUtils(IConfiguration config, IDataBase dataBase)
    {
        _config = config;
        _dataBase = dataBase;
    }

    public string GetJWTSecret()
    {
        return _config.GetValue<string>("JWT_SECRET");
    }
    public List<Claim> GetClaimsFromJWT(string token)
    {
        string secret = GetJWTSecret();
        var key = Encoding.UTF8.GetBytes(secret);
        var handler = new JwtSecurityTokenHandler();
        var validations = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        return handler.ValidateToken(token, validations, out var tokenSecure).Claims.ToList();
    }

    public void AuthenticateToken(string token, out string username, out string email)
    {
        token = token.Replace("bearer ", "");
        var claims = GetClaimsFromJWT(token);
        if (!ValidateClaimsAuthenticity(claims)) throw new Exception("Incorrect credentials");
        GetUsernameAndEmailFromClaims(claims, out username, out  email);
    }

    public bool ValidateClaimsAuthenticity(List<Claim> claims)
    {
        string dateString = claims.First(x => x.Type == "exp").Value;
        var date = epoch.AddSeconds(Convert.ToInt64(dateString));
        if (date < DateTime.Now) throw new Exception("Token Expired");
        try
        {
            GetUsernameAndEmailFromClaims(claims, out string username, out string email);
            return _dataBase.CheckClaims(username, email);
        }
        catch (Exception ex)
        {
            throw new Exception("Incorrect credentials");
        };
    }

    public void GetUsernameAndEmailFromClaims(List<Claim> claims, out string username, out string email)
    {
        username = claims.First(x => x.Type == ClaimTypes.Name).Value;
        email = claims.First(x => x.Type == ClaimTypes.Email).Value;
    }
}
