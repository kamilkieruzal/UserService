using AutoMapper;
using UserService.DTOs;
using UserService.Interfaces.Models;
using UserService.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IDbService dbService;
        private readonly IValidationService validationService;
        private readonly IRandomUserService randomUserService;

        public UserDataController(IMapper mapper, IDbService dbService, IValidationService validationService, IRandomUserService randomUserService)
        {
            this.mapper = mapper;
            this.dbService = dbService;
            this.validationService = validationService;
            this.randomUserService = randomUserService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveUserData([FromBody] UserDataDTO userDataDTO)
        {
            await dbService.DeactivateOldUserDataAsync(); //instead this in real db could be implemented as some db job running stored procedure

            try
            {
                var userData = mapper.Map<UserDataModel>(userDataDTO);
                if (!await validationService.IsUserDataValidAsync(userData))
                    return BadRequest("Invalid user data");

                await dbService.AddNewUserDataAsync(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("User data record created");
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters, [FromQuery] string? userName = null)
        {
            await dbService.DeactivateOldUserDataAsync(); //instead this in real db could be implemented as some db job running stored procedure

            IEnumerable<UserDataModel> result;

            try
            {
                result = await dbService.GetUserDataAsync(userParameters, userName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpGet("randomUserData")]
        public IActionResult CreateRandomUserData([FromQuery] UserDTO user)
        {
            var userModel = mapper.Map<UserModel>(user);
            var randomUserData = mapper.Map<UserDataDTO>(randomUserService.GetRandomUserData(userModel));

            return Ok(randomUserData);
        }
    }
}