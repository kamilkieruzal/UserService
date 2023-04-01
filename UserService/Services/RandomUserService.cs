using Bogus;
using UserService.Interfaces.Models;
using UserService.Interfaces.Services;

namespace UserService.Services
{
    public class RandomUserService : IRandomUserService
    {
        private readonly int nameLength = 256;
        private int userId = 1;

        public UserModel GetRandomUser()
        {
            var randomUser = new Faker<UserModel>()
                .RuleFor(u => u.UserId, f => userId++)
                .RuleFor(u => u.Name, f => f.Random.String(nameLength, 'A', 'z'));

            var user = new UserModel();
            randomUser.Populate(user);

            return user;
        }
    }
}
