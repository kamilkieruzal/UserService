using Microsoft.EntityFrameworkCore;
using UserService.Database;
using UserService.Interfaces.Models;
using UserService.Interfaces.Services;

namespace UserService.Services
{
    public class DbService : IDbService
    {
        private const int NumberOfDaysForUserDataActiveStatus = 5;
        private readonly IRandomUserService randomUserService;
        private readonly UserDbContext dbContext;

        public DbService(IRandomUserService randomUserService, UserDbContext dbContext)
        {
            this.randomUserService = randomUserService;
            this.dbContext = dbContext;
        }

        public async Task AddNewUserDataAsync(UserDataModel userDataModel)
        {
            userDataModel.Status = true;

            if (userDataModel.CreatedDate == DateTime.MinValue)
                userDataModel.CreatedDate = DateTime.Now.Date;

            await dbContext.UserData.AddAsync(userDataModel);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddNewUserAsync(UserModel userModel)
        {
            await dbContext.Users.AddAsync(userModel);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync(UserParameters userParameters, string? userName = null)
        {
            var database = string.IsNullOrWhiteSpace(userName) ? dbContext.Users : dbContext.Users.Where(u => u.UserName.Equals(userName));

            return await database.OrderBy(u => u.Id)
                .Skip((userParameters.PageNumber - 1) * userParameters.PageSize)
                .Take(userParameters.PageSize).ToListAsync();
        }

        public async Task<IEnumerable<UserDataModel>> GetUserDataAsync(UserParameters userParameters, string? userName = null)
        {
            var database = string.IsNullOrWhiteSpace(userName) ? dbContext.UserData : dbContext.UserData.Where(u => u.User.UserName.Equals(userName));

            return await dbContext.UserData.OrderBy(u => u.Id)
                .Skip((userParameters.PageNumber - 1) * userParameters.PageSize)
                .Take(userParameters.PageSize).ToListAsync();
        }

        public async Task PopulateDatabaseAsync()
        {
            var user1 = randomUserService.GetRandomUser();
            var user2 = randomUserService.GetRandomUser();
            var user3 = randomUserService.GetRandomUser();
            var user4 = randomUserService.GetRandomUser();
            var user5 = randomUserService.GetRandomUser();

            await AddNewUserAsync(user1);
            await AddNewUserAsync(user2);
            await AddNewUserAsync(user3);
            await AddNewUserAsync(user4);
            await AddNewUserAsync(user5);

            for (var i = 0; i < 5; i++)
            {
                var randomUser1Data = randomUserService.GetRandomUserData(user1);
                randomUser1Data.CreatedDate = DateTime.Now.Date;
                await AddNewUserDataAsync(randomUser1Data);
            }

            await AddNewUserDataAsync(randomUserService.GetRandomUserData(user2));
            await AddNewUserDataAsync(randomUserService.GetRandomUserData(user2));
            await AddNewUserDataAsync(randomUserService.GetRandomUserData(user2));
            await AddNewUserDataAsync(randomUserService.GetRandomUserData(user2));

            var randomUser3Data = randomUserService.GetRandomUserData(user3);
            randomUser3Data.CreatedDate = DateTime.Now.Date.AddDays(NumberOfDaysForUserDataActiveStatus);
            await AddNewUserDataAsync(randomUser3Data);

            var randomUser4Data = randomUserService.GetRandomUserData(user4);
            randomUser4Data.CreatedDate = DateTime.Now.Date.AddDays(-(NumberOfDaysForUserDataActiveStatus + 1));
            await AddNewUserDataAsync(randomUser4Data);
        }

        public async Task DeactivateOldUserDataAsync()
        {
            var today = DateTime.Now.Date;

            foreach (var user in dbContext.UserData.Where(x => (today - x.CreatedDate) > TimeSpan.FromDays(NumberOfDaysForUserDataActiveStatus)))
                user.Status = false;

            await dbContext.SaveChangesAsync();
        }
    }
}
