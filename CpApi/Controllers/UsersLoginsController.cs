using Microsoft.AspNetCore.Mvc;
using CpApi.Interfaces;
using CpApi.Requests;
using CpApi.Model;
using Microsoft.AspNetCore.Authorization;

namespace CpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersLoginsController : ControllerBase
    {
        private readonly IUsersLoginsService _userLoginService;

        public UsersLoginsController(IUsersLoginsService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        [HttpGet]
        [Route("getAllUsers")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            return await _userLoginService.GetAllUsersAsync();
        }

        [HttpPost]
        [Route("createNewUserAndLogin")]
        public async Task<IActionResult> CreateNewUserAndLogin(CreateNewUserAndLogin newUser)
        {
            return await _userLoginService.CreateNewUserAndLoginAsync(newUser);
        }

        [HttpGet]
        [Route("Authorization")]
        public async Task<IActionResult> Authorization(string email, string pass)
        {
            return await _userLoginService.AuthorizationAsync(email, pass);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            return await _userLoginService.DeleteUserAsync(Id);
        }

        [HttpPost]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser(Users user, string email, string pass)
        {
            return await _userLoginService.EditUserAsync(user, email, pass);
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return await _userLoginService.GetUsersAsync();
        }

    }
}
