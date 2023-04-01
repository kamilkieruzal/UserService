using Microsoft.EntityFrameworkCore;
using UserService.Database;
using UserService.Interfaces.Models;
using UserService.Interfaces.Services;

namespace UserService.Services
{
    public class ValidationService : IValidationService
    {
        private const int NumberOfPossibleUserDataToday = 5;
        private readonly UserDbContext userDbContext;

        public ValidationService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<bool> IsUserDataValidAsync(UserDataModel userDataModel)
        {
            if (await userDbContext.UserData
                .Where(u => u.CreatedDate == DateTime.Today.Date)
                .CountAsync(x => x.User.Id == userDataModel.UserId) >= NumberOfPossibleUserDataToday)
                return false;
            return true;
        }
    }
}
