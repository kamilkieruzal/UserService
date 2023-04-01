using UserService.Interfaces.Models;

namespace UserService.Interfaces.Services
{
    public interface IRandomUserService
    {
        UserModel GetRandomUser();
    }
}
