using CcsApi.Authentication.Interfaces;
using CcsApi.Authentication.Models;
using CcsApi.Models;

namespace CcsApi.Authentication
{
    public class UserService : IUserService
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "cat", Password = "catontheroof" }
        };

        public bool IsValidLogin(User user)
        {
            return _users.Any(u => u.Username == user.Username && u.Password == user.Password);
        }
    }
}
