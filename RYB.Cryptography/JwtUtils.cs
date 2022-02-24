using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RYB.Model.AppSettings;
using RYB.Model.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RYB.Cryptography;

public class JwtUtils : IJwtUtils
{
    private readonly Jwt jwtAppSettings;

    public JwtUtils(
        IOptions<Jwt> jwtSettings)
    {
        jwtAppSettings = jwtSettings.Value;
    }

    public string GenerateJwtToken(UserToken user)
    {
        // generate token that is valid for 15 minutes
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtAppSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(string ipAddress)
    {
        var refreshToken = new RefreshToken
        {
            Token = getUniqueToken(),
            // token is valid for 7 days
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };

        return refreshToken;

        string getUniqueToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            // ensure token is unique by checking against db
            // var tokenIsUnique = !_context.Users.Any(u => u.RefreshTokens.Any(t => t.Token == token));
            var tokenIsUnique = true;
            if (!tokenIsUnique)
                return getUniqueToken();

            return token;
        }
    }

    public string ValidateJwtToken(string token)
    {
        if (token == null)
            return "";

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtAppSettings.Secret);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            // return user id from JWT token if validation successful
            return userId;
        }
        catch
        {
            // return Empty if validation fails
            return "";
        }
    }
}
