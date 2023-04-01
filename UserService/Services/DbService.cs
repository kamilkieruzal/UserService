using UserService.Interfaces.Models;
using UserService.Interfaces.Services;

namespace UserService.Services
{
    public class DbService : IDbService
    {
        public DbService()
        {
            UserDatabase = new List<UserModel>();
        }

        public IList<UserModel> UserDatabase { get; set; }

        public void AddNewUser(UserModel userModel)
        {
            userModel.Status = true;
            userModel.CreatedDate = DateTime.Now.Date;

            UserDatabase.Add(userModel);
        }
    }
}
