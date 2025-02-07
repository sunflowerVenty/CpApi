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

        [HttpPost]
        [Route("Authorization")]
        public async Task<IActionResult> Authorization([FromBody] AuthUser authuser)
        {
            return await _userLoginService.AuthorizationAsync(authuser);
        }

        [HttpDelete]
        [Route("DeleteUser/{Id}")]
        public async Task<IActionResult> DeleteUer(int Id)
        {
            return await _userLoginService.DeleteUserAsync(Id);
        }

        [HttpPut]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] UserInfo userInfo)
        {
            return await _userLoginService.EditUserAsync(userInfo);
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return await _userLoginService.GetUsersAsync();
        }

    }
}
