using CpApi.DataBaseContext;
using CpApi.Interfaces;
using CpApi.Model;
using CpApi.Requests;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpApi.Service
{
    public class MessageService : IMessageService
    {
        private readonly ContextDb _context;

        public MessageService(ContextDb context)
        {
            _context = context;
        }

        // Получить все сообщения
        public async Task<IEnumerable<Messages>> GetAllMessagesAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        // Получить сообщение по ID
        public async Task<Messages> GetMessageByIdAsync(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        // Создать новое сообщение
        public async Task<Messages> CreateMessageAsync(AddMessageRequest messageRequest)
        {
            var message = new Messages
            {
                Message = messageRequest.Message,
                Film_Id = messageRequest.Film_Id,
                User_Id = messageRequest.User_Id,
                Recipient_Id = messageRequest.Recipient_Id,
                ImageURL = messageRequest.ImageURL,
                dateTimeSent = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        // Обновить существующее сообщение
        public async Task<bool> UpdateMessageAsync(UpdateMessageRequest messageRequest)
        {
            var existingMessage = await _context.Messages.FindAsync(messageRequest.Id_Message);
            if (existingMessage == null)
            {
                return false;
            }

            existingMessage.Message = messageRequest.Message;
            existingMessage.ImageURL = messageRequest.ImageURL;

            await _context.SaveChangesAsync();
            return true;
        }

        // Удалить сообщение
        public async Task<bool> DeleteMessageAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return false;

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }

        // Получить все сообщения для конкретного фильма
        public async Task<IEnumerable<Messages>> GetMessagesByFilmIdAsync(int filmId)
        {
            return await _context.Messages
                .Where(m => m.Film_Id == filmId)
                .OrderByDescending(m => m.dateTimeSent)
                .ToListAsync();
        }

        // Получить все сообщения между двумя пользователями (чат)
        public async Task<IEnumerable<Messages>> GetChatMessagesAsync(int userId, int recipientId)
        {
            return await _context.Messages
                .Where(m => (m.User_Id == userId && m.Recipient_Id == recipientId) ||
                            (m.User_Id == recipientId && m.Recipient_Id == userId))
                .OrderBy(m => m.dateTimeSent)
                .ToListAsync();
        }

        // Получить последние сообщения из всех чатов пользователя
        public async Task<IEnumerable<Messages>> GetLatestMessagesForUserAsync(int userId)
        {
            return await _context.Messages
                .Where(m => m.User_Id == userId || m.Recipient_Id == userId)
                .GroupBy(m => new { m.User_Id, m.Recipient_Id })
                .Select(g => g.OrderByDescending(m => m.dateTimeSent).FirstOrDefault())
                .ToListAsync();
        }
    }
}
