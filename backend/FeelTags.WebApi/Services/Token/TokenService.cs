using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FeelTags.WebApi.Services.Token
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<KeyValuePair<string, string>> claims, DateTime? expires = null);
    }

    public class TokenService : ITokenService
    {
        private readonly ILogger _logger;
        private readonly SymmetricSecurityKey _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(ILogger<ITokenService> logger, IOptions<TokenServiceSettings> tokenOptions)
        {
            _logger = logger;
            TokenServiceSettings settings = tokenOptions.Value;
            ArgumentNullException.ThrowIfNull(settings, nameof(settings));

            _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
            _issuer = settings.Issuer;
            _audience = settings.Audience;

        }
        public string GenerateToken(IEnumerable<KeyValuePair<string, string>> claims, DateTime? expires = null)
        {
            _logger.LogInformation("Token service started GenerateToken");

            SigningCredentials signinCredentials = new(_secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenOptions = new(
                issuer: _issuer,
                audience: _audience,
                claims: claims.Select(x => new Claim(x.Key, x.Value)),
                expires: expires ?? DateTime.MaxValue,
                signingCredentials: signinCredentials
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
    }
}
