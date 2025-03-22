using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CpApi.Requests
{
    public class AddMessageRequest
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public int Film_Id { get; set; }

        [Required]
        public int User_Id { get; set; }

        
        public int ?Recipient_Id { get; set; }

        public DateTime dateTimeSent { get; set; }
        public string ImageURL { get; set; }
    }
}
