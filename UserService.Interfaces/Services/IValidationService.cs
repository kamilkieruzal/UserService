using UserService.Interfaces.Models;

namespace UserService.Interfaces.Services
{
    public interface IValidationService
    {
        bool IsValid(UserModel userModel, IList<UserModel> database);
        void DeactivateOldUsers(IList<UserModel> database);
    }
}
