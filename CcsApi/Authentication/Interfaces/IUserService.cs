using CcsApi.Authentication.Models;

namespace CcsApi.Authentication.Interfaces
{
    public interface IUserService
    {
        bool IsValidLogin(User user);
    }
}
