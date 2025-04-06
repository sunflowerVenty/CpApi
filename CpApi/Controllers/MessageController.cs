﻿using CpApi.DataBaseContext;
using CpApi.Interfaces;
using CpApi.Requests;
using CpApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly ContextDb _context;

        public MessageController(ContextDb context)
        {
            _context = context;
        }

        [HttpGet("getMessages/{userId}")]
        public async Task<List<UserMessageDto>> GetMessages(int userId)
        {
            var userPairs = await _context.Messages
            .Where(m => m.SenderId == userId || m.ReceiverId == userId)
            .Select(m => new
            {
                User1Id = m.SenderId < m.ReceiverId ? m.SenderId : m.ReceiverId,
                User2Id = m.SenderId < m.ReceiverId ? m.ReceiverId : m.SenderId
            })
            .Distinct()
            .ToListAsync();

            var userIds = userPairs.Select(up => new { up.User1Id, up.User2Id }).ToList();

            var users = await _context.Users
                .Where(u => userIds.Select(x => x.User1Id).Contains(u.Id) || userIds.Select(x => x.User2Id).Contains(u.Id))
                .ToListAsync();

            var result = userPairs.Select(up => new UserMessageDto
            {
                SenderId = up.User1Id,
                ReceiverId = up.User2Id,
                SenderName = users.FirstOrDefault(u => u.Id == up.User1Id)?.Name,
                ReceiverName = users.FirstOrDefault(u => u.Id == up.User2Id)?.Name
            }).ToList();

            return result;
        }

        [HttpGet("getMessagesWithUser/{userId}/{senderId}")]
        public async Task<List<MessagesDto>> getMessagesWithUser(int userId, int senderId)
        {
            var messages = await _context.Messages
            .Where(m => (m.SenderId == userId && m.ReceiverId == senderId) ||
                         (m.SenderId == senderId && m.ReceiverId == userId))
            .OrderBy(m => m.Timestamp)
            .Select(m => new MessagesDto
            {
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Message = m.Message,
                Timestamp = m.Timestamp,
                SenderName = _context.Users.FirstOrDefault(u => u.Id == m.SenderId).Name,
                ReceiverName = _context.Users.FirstOrDefault(u => u.Id == m.ReceiverId).Name
            })
            .ToListAsync();

            return messages;
        }

        [HttpGet("getMessagesFilms/{id:int}")]
        public async Task<List<MessagesFilmDto>> GetMessagesFilms()
        {
            var messages = await _context.ChatFilm
            .Select(m => new
            {
                m.MovieId,
                m.SenderId,
                m.Message,
                FilmName = m.Movie.Name,
                SenderName = m.Users.Name,
                Timestamp = m.Timestamp
            })
            .ToListAsync();

            var result = messages.Select(m => new MessagesFilmDto
            {
                MovieId = m.MovieId,
                SenderId = m.SenderId,
                Message = m.Message,
                Title = m.FilmName,
                SenderName = m.SenderName,
                Timestamp = m.Timestamp
            }).ToList();

            return result;
        }

        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Файл не выбран.");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/uploads/{fileName}";
            return Ok(new { imageUrl });
        }

        public class UserMessageDto
        {
            public int SenderId { get; set; }
            public string SenderName { get; set; }
            public int ReceiverId { get; set; }
            public string ReceiverName { get; set; }
        }

        public class MessagesDto
        {
            public int SenderId { get; set; }
            public string SenderName { get; set; }
            public int ReceiverId { get; set; }
            public string ReceiverName { get; set; }

            public string Message { get; set; }

            public DateTime Timestamp { get; set; }
        }

        public class MessagesFilmDto
        {
            public int? MovieId { get; set; }
            public int SenderId { get; set; }
            public string SenderName { get; set; }
            public string? Title { get; set; }
            public string Message { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}
