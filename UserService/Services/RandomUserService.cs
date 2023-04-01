using Bogus;
using UserService.Interfaces.Models;
using UserService.Interfaces.Services;

namespace UserService.Services
{
    public class RandomUserService : IRandomUserService
    {
        private readonly int dataNameLength = 256;
        private readonly int userNameLength = 5;
        private int userId = 1;
        private int userDataId = 1;

        public UserModel GetRandomUser()
        {
            var randomUser = new Faker<UserModel>()
                .RuleFor(u => u.Id, f => userId++)
                 .RuleFor(d => d.UserName, f => f.Random.String(userNameLength, 'A', 'Z'))
;

            var user = new UserModel();
            randomUser.Populate(user);

            return user;
        }

        public UserDataModel GetRandomUserData(UserModel user)
        {
            var randomData = new Faker<UserDataModel>()
                .RuleFor(d => d.Id, f => userDataId++)
                .RuleFor(d => d.Name, f => f.Random.String(dataNameLength, 'A', 'z'))
                .RuleFor(d => d.User, f => user)
                .RuleFor(d => d.UserId, f => user.Id)
                .RuleFor(d => d.Status, f => true)
                .RuleFor(d => d.CreatedDate, f => f.Date.Past(1).Date);

            var data = new UserDataModel();
            randomData.Populate(data);

            return data;
        }
    }
}
