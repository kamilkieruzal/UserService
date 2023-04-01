using AutoMapper;
using UserService.DTOs;
using UserService.Interfaces.Models;
using UserService.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IDbService dbService;
        private readonly IValidationService validationService;
        private readonly IRandomUserService randomUserService;

        public UserController(IMapper mapper, IDbService dbService, IValidationService validationService, IRandomUserService randomUserService)
        {
            this.mapper = mapper;
            this.dbService = dbService;
            this.validationService = validationService;
            this.randomUserService = randomUserService;
        }

        [HttpPost]
        public void SaveUser([FromBody] UserDTO userDTO)
        {
            var user = mapper.Map<UserModel>(userDTO);
            if (validationService.IsValid(user, dbService.UserDatabase))
                dbService.AddNewUser(user);

            validationService.DeactivateOldUsers(dbService.UserDatabase);
        }

        [HttpGet]
        public IEnumerable<UserModel> GetUsers()
        {
            return dbService.UserDatabase;
        }

        [HttpGet("RandomUser")]
        public UserDTO GetRandomUser()
        {
            return mapper.Map<UserDTO>(randomUserService.GetRandomUser());
        }
    }
}