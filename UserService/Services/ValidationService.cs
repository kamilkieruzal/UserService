using UserService.Interfaces.Models;
using UserService.Interfaces.Services;

namespace UserService.Services
{
    public class ValidationService : IValidationService
    {
        public bool IsValid(UserModel userModel, IList<UserModel> database)
        {
            if (database.Count(x => x.UserId == userModel.UserId) > 4)
                return false;
            return true;
        }

        public void DeactivateOldUsers(IList<UserModel> database)
        {
            var today = DateTime.Now.Date;

            foreach (var user in database.Where(x => (today - x.CreatedDate) > TimeSpan.FromDays(4)))
            {
                user.Status = false;
            }
        }
    }
}
