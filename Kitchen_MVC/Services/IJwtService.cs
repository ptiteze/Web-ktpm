using System.Security.Claims;

namespace Kitchen_MVC.Services
{
    public interface IJwtService
    {
        Task<string> CreateJWT(string email);

        ClaimsPrincipal validateExpiredJwt(string token);

        string CreateRefreshToken();
    }
}
