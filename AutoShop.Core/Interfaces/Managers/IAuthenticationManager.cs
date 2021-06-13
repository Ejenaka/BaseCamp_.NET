using AutoShop.Core.Models;

namespace AutoShop.Core.Interfaces.Managers
{
    public interface IAuthenticationManager
    {
        User Authenticate(User user, UserLoginModel loginModel);
    }
}