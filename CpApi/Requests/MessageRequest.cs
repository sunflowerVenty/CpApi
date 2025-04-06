using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CpApi.Requests
{
    public class MessageRequest
    {
        public string Message { get; set; }
        public string? ImageURL { get; set; }
        public int? Film_Id { get; set; }
        public int? Recipient_Id { get; set; }
        public int? User_Id { get; set; }
    }
}
