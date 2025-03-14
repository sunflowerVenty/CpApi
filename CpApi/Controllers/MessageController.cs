using CpApi.Interfaces;
using CpApi.Requests;
using CpApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messagesService;

        public MessageController(MessageService messagesService)
        {
            _messagesService = messagesService;
        }

        /// <summary>
        /// Получить все сообщения
        /// </summary>
        [HttpGet]
        [Route("GetAllMessages")]
        public async Task<IActionResult> GetAllMessages()
        {
            var messages = await _messagesService.GetAllMessagesAsync();
            return Ok(messages);
        }

        /// <summary>
        /// Получить сообщение по ID
        /// </summary>
        [HttpGet]
        [Route("GetMessageById/{id}")]
        public async Task<IActionResult> GetMessageById(int id)
        {
            var message = await _messagesService.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound($"Сообщение с ID {id} не найдено.");
            }
            return Ok(message);
        }

        /// <summary>
        /// Создать новое сообщение
        /// </summary>
        [HttpPost]
        [Route("CreateMessage")]
        public async Task<IActionResult> CreateMessage([FromBody] AddMessageRequest newMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdMessage = await _messagesService.CreateMessageAsync(newMessage);
            return CreatedAtAction(nameof(GetMessageById), new { id = createdMessage.Id_Message }, createdMessage);
        }

        /// <summary>
        /// Обновить существующее сообщение
        /// </summary>
        [HttpPut]
        [Route("UpdateMessage")]
        public async Task<IActionResult> UpdateMessage([FromBody] UpdateMessageRequest updatedMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _messagesService.UpdateMessageAsync(updatedMessage);
            if (!success)
            {
                return NotFound($"Сообщение с ID {updatedMessage.Id_Message} не найдено.");
            }
            return NoContent();
        }

        /// <summary>
        /// Удалить сообщение по ID
        /// </summary>
        [HttpDelete]
        [Route("DeleteMessage/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var success = await _messagesService.DeleteMessageAsync(id);
            if (!success)
            {
                return NotFound($"Сообщение с ID {id} не найдено.");
            }
            return NoContent();
        }

        /// <summary>
        /// Получить все сообщения для конкретного фильма
        /// </summary>
        [HttpGet]
        [Route("GetMessagesByFilmId/{filmId}")]
        public async Task<IActionResult> GetMessagesByFilmId(int filmId)
        {
            var messages = await _messagesService.GetMessagesByFilmIdAsync(filmId);
            return Ok(messages);
        }

        /// <summary>
        /// Получить все сообщения между двумя пользователями (чат)
        /// </summary>
        [HttpGet]
        [Route("GetChatMessages")]
        public async Task<IActionResult> GetChatMessages([FromQuery] int userId, [FromQuery] int recipientId)
        {
            var messages = await _messagesService.GetChatMessagesAsync(userId, recipientId);
            return Ok(messages);
        }

        /// <summary>
        /// Получить последние сообщения из всех чатов пользователя
        /// </summary>
        [HttpGet]
        [Route("GetLatestMessagesForUser/{userId}")]
        public async Task<IActionResult> GetLatestMessagesForUser(int userId)
        {
            var messages = await _messagesService.GetLatestMessagesForUserAsync(userId);
            return Ok(messages);
        }
    }
}
