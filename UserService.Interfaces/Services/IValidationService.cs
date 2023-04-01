using UserService.Interfaces.Models;

namespace UserService.Interfaces.Services
{
    public interface IValidationService
    {
        Task<bool> IsUserDataValidAsync(UserDataModel userDataModel);
    }
}
