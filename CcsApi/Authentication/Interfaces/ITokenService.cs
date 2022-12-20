using CcsApi.Authentication.Models;

namespace CcsApi.Authentication.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user, string key, string issuer, string audience);
    }
}
