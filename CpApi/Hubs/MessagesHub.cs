using CpApi.DataBaseContext;
using CpApi.Model;
using Microsoft.AspNetCore.SignalR;

namespace CpApi.Hubs
{
    public class MessagesHub : Hub
    {
        private readonly ContextDb _context;

        public MessagesHub(ContextDb context)
        {
            _context = context;
        }


        public async Task SendMessageToUser(string message, int idUser, int idReceiver, string imageUrl = null)
        {
            var receiver = await _context.Users.FindAsync(idReceiver);
            if (receiver != null)
            {
                string connection = receiver.ConnectionId;
                try
                {
                    // await Clients.User(connection). ("ReceiveMessage", message, idUser, idReceiver);
                    await Clients.All.SendAsync("ReceiveMessage", message, idUser, idReceiver, imageUrl);
                    _context.Messages.Add(new Messages
                    {
                        SenderId = idUser,
                        ReceiverId = idReceiver,
                        Message = message,
                        ImageUrl = imageUrl

                    });
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Good sending");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error sending message: {e.Message}");
                }
            }
        }

        public async Task SendMessageFilm(string message, int senderId, int? idFilm)
        {
            var user = await _context.Users.FindAsync(senderId);
            string Title = null;
            if (idFilm != null) Title = (await _context.Movies.FindAsync(idFilm)).Name;

            _context.ChatFilm.Add(new ChatFilm
            {
                SenderId = senderId,
                MovieId = idFilm,
                Message = message
            });
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessageFilm", message, senderId, user.Name, Title);
        }

        public async Task RegisterUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                try
                {
                    user.ConnectionId = Context.ConnectionId;
                    await _context.SaveChangesAsync();
                }
                catch (Exception e) { }
            }
        }
    }
}
