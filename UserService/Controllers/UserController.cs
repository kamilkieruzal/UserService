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
        private readonly IRandomUserService randomUserService;

        public UserController(IMapper mapper, IDbService dbService, IRandomUserService randomUserService)
        {
            this.mapper = mapper;
            this.dbService = dbService;
            this.randomUserService = randomUserService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser([FromBody] UserDTO userDTO)
        {
            await dbService.DeactivateOldUserDataAsync(); //instead this in real db could be implemented as some db job running stored procedure

            try
            {
                var user = mapper.Map<UserModel>(userDTO);
                await dbService.AddNewUserAsync(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("User record created");
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters, [FromQuery] string? userName = null)
        {
            await dbService.DeactivateOldUserDataAsync(); //instead this in real db could be implemented as some db job running stored procedure

            IEnumerable<UserDTO> result;

            try
            {
                var users = await dbService.GetUsersAsync(userParameters, userName);
                result = mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpGet("randomUser")]
        public IActionResult CreateRandomUser()
        {
            var randomUser = mapper.Map<UserDTO>(randomUserService.GetRandomUser());

            return Ok(randomUser);
        }

        [HttpPost("populateDb")]
        public async Task<IActionResult> PopulateDatabase()
        {
            await dbService.PopulateDatabaseAsync();

            return Ok();
        }
    }
}