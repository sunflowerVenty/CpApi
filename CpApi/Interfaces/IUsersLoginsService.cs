using Microsoft.AspNetCore.Mvc;
using CpApi.Requests;
using CpApi.Model;
using static CpApi.Service.UserLoginService;

namespace CpApi.Interfaces
{
    public interface IUsersLoginsService
    {
        Task<IActionResult> Register([FromBody] RegisterUser request);
        Task<IActionResult> Login([FromBody] AuthUser request);
        Task<IActionResult> GetUsers();
        Task<UserDto?> GetUserIdFromTokenAsync(string token);
        Task<IActionResult> UpdateUser([FromBody] UpdateUser request);
        Task<IActionResult> DeleteUser([FromBody] ChangeRoleAndDeleteRequest request);
        Task<IActionResult> GetUserById(int id);
        Task<IActionResult> ChangeUserRole([FromBody] ChangeRoleAndDeleteRequest request);

    }
}
