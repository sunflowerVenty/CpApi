using CpApi.Interfaces;
using CpApi.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using static CpApi.Service.UserLoginService;

namespace CpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController
    {
        private readonly IUsersLoginsService _userLoginService;
        public UserLoginController(IUsersLoginsService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return await _userLoginService.GetUsers();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser request)
        {
            return await _userLoginService.Register(request);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthUser request)
        {
            return await _userLoginService.Login(request);
        }

        [HttpPut("ChangeUserRole")]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeRoleAndDeleteRequest request)
        {
            return await _userLoginService.ChangeUserRole(request);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return await _userLoginService.GetUserById(id);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] ChangeRoleAndDeleteRequest request)
        {
            return await _userLoginService.DeleteUser(request);
        }

        [HttpGet("GetUserIdFromToken")]
        public async Task<UserDto?> GetUserIdFromTokenAsync(string token)
        {
            return await _userLoginService.GetUserIdFromTokenAsync(token);
        }

        [HttpPut("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser request)
        {
            return await _userLoginService.UpdateUser(request);
        }
    }
}
