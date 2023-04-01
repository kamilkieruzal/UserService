using UserService.Interfaces.Models;

namespace UserService.Interfaces.Services
{
    public interface IDbService
    {
        Task AddNewUserAsync(UserModel userModel);
        Task AddNewUserDataAsync(UserDataModel userDataModel);
        Task<IEnumerable<UserModel>> GetUsersAsync(UserParameters userParameters, string? userName = null);
        Task<IEnumerable<UserDataModel>> GetUserDataAsync(UserParameters userParameters, string? userName = null);
        Task PopulateDatabaseAsync();
        Task DeactivateOldUserDataAsync();
    }
}
