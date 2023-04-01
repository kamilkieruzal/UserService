using UserService.Interfaces.Models;

namespace UserService.Interfaces.Services
{
    public interface IDbService
    {
        void AddNewUser(UserModel userModel);
        IList<UserModel> UserDatabase { get; set; }
    }
}
