using Microsoft.AspNetCore.Mvc;
using CpApi.Requests;
using CpApi.Model;

namespace CpApi.Interfaces
{
    public interface IUsersLoginsService
    {
        Task<IActionResult> GetAllUsersAsync();
        Task<IActionResult> CreateNewUserAndLoginAsync(CreateNewUserAndLogin newUser);
        Task<IActionResult> AuthorizationAsync(string email, string pass);
        Task<IActionResult> DeleteUserAsync(int Id);
        Task<IActionResult> EditUserAsync(Users user, string email, string pass);
        Task<IActionResult> GetUsersAsync();

    }
}
