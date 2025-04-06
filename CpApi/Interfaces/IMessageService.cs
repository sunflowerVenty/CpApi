using CpApi.Model;
using CpApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CpApi.Interfaces
{
    public interface IMessageService
    {
        Task<IActionResult> GetMessagesForMovieAsync(int movieId);
        Task<IActionResult> GetPrivateMessagesAsync(int userId, int recipientId);
        Task<IActionResult> PostMessageAsync([FromBody] MessageRequest messageDto);
        Task<IActionResult> EditMessageAsync(int id, EditMessageRequest editRequest);
        Task<IActionResult> DeleteMessageAsync(int id);
        Task<IActionResult> UploadImageAsync(IFormFile file);
        Task<IActionResult> GetUserDialogsAsync(int userId);
        Task<IActionResult> SendPrivateMessageAsync(int senderId, int recipientId, string message, string imageUrl = null);
    }
}
