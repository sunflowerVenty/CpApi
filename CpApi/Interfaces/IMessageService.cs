﻿using CpApi.Model;
using CpApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CpApi.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Messages>> GetAllMessagesAsync();
        Task<Messages> GetMessageByIdAsync(int id);
        Task<IActionResult> CreateMessageAsync([FromBody] AddMessageRequest message);
        Task<bool> UpdateMessageAsync(UpdateMessageRequest message);
        Task<bool> DeleteMessageAsync(int id);
        Task<IEnumerable<Messages>> GetMessagesByFilmIdAsync(int filmId);
        Task<IEnumerable<Messages>> GetChatMessagesAsync(int userId, int recipientId);
        Task<IEnumerable<Messages>> GetLatestMessagesForUserAsync(int userId);
    }
}
