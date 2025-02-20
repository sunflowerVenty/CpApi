using Microsoft.AspNetCore.Mvc;
using CpApi.Requests;
using CpApi.Model;

namespace CpApi.Interfaces
{
    public interface IUsersLoginsService
    {
        Task<IActionResult> GetAllUsersAsync();
        Task<IActionResult> CreateNewUserAndLoginAsync([FromBody] CreateNewUserAndLogin newUser);
        Task<IActionResult> AuthorizationAsync([FromBody] AuthUser user);
        Task<IActionResult> DeleteUserAsync(int Id);
        Task<IActionResult> EditUserAsync([FromBody] UserInfo userInfo);
        Task<IActionResult> GetUsersAsync();

    }
}
