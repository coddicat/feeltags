using FeelTags.WebApi.Dal.DTOs;
using FeelTags.WebApi.Dal.Entities;
using FeelTags.WebApi.Dal.Repositories;
using FeelTags.WebApi.Services.Token;
using FirebaseAdmin.Auth;
using System.Security.Claims;

namespace FeelTags.WebApi.ApiServices
{
    public interface IAuthApiService
    {
        Task<AccountDTO?> CheckAsync(HttpContext httpContext);
        Task<AccountDTO> SignInWithGoogleAsync(HttpContext httpContext, string idToken);
    }
    public class AuthApiService(        
        IWebHostEnvironment webHostEnvironment, 
        IAccountRepo accountRepo,
        ITokenService tokenService) : IAuthApiService
    {        
        public async Task<AccountDTO> SignInWithGoogleAsync(HttpContext httpContext, string idToken)
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            string uid = decodedToken.Uid;

            Dictionary<string, string> firebaseClaims = decodedToken.Claims
                .Where(x => x.Value is not null)
                .ToDictionary(x => x.Key, x => $"{x.Value}");

            string name = GetStringClaim(firebaseClaims, FirebaseClaimTypes.Name, "N/A");
            string email = GetStringClaim(firebaseClaims, FirebaseClaimTypes.Email, "N/A"); 
            string picture = GetStringClaim(firebaseClaims, FirebaseClaimTypes.Picture, string.Empty);

            AccountDTO dto = new () { Email = email, Name = name, Picture = picture };
            await accountRepo.CreateOrUpdateFirebaseAccountAsync(uid, dto);

            var claims = GetAuthClaims(uid, dto, firebaseClaims);
            string accessToken = tokenService.GenerateToken(claims);
            SetAccessToken(httpContext, accessToken);

            return dto;
        }

        private IEnumerable<KeyValuePair<string, string>> GetAuthClaims(string uid, AccountDTO dto, IDictionary<string, string> firebaseClaims)
        {
            var defaultClaims = new Dictionary<string, string>
            {
                { ClaimTypes.NameIdentifier, uid },
                { ClaimTypes.Name, dto.Name },
                { ClaimTypes.Email, dto.Email },
                { ClaimTypes.AuthenticationMethod, "firebase" }
            };
            IEnumerable<KeyValuePair<string, string>> claims = firebaseClaims.Concat(defaultClaims);
            return claims;
        }

        private static string GetStringClaim(IDictionary<string, string> claims, string claimName, string emptyValue)
        {
            return claims.TryGetValue(claimName, out string? nameValue) ? nameValue : emptyValue;
        }

        private void SetAccessToken(HttpContext httpContext, string token)
        {
            httpContext.Response.Headers.Authorization = $"Bearer {token}";
            httpContext.Response.Cookies.Append("AccessToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = !webHostEnvironment.IsDevelopment(),
                SameSite = SameSiteMode.Strict,
            });
        }

        public async Task<AccountDTO?> CheckAsync(HttpContext httpContext)
        {
            ClaimsPrincipal principal = httpContext.User;            
            if (principal.Identity?.IsAuthenticated != true)
            {
                return null;
            } 
            string? uid = principal.FindFirstValue("user_id");
            if (string.IsNullOrWhiteSpace(uid))
            {
                return null;
            }

            Account? dbAccount = await accountRepo.GetFirebaseAccountAsync(uid);
            if (dbAccount is null)
            {
                return null;
            }

            return AccountDTO.FromDbAccount(dbAccount);
        }
    }
}
