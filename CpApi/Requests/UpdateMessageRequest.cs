using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CpApi.Requests
{
    public class UpdateMessageRequest
    {
        [Required]
        public int Id_Message { get; set; }

        public string Message { get; set; }

        public string ImageURL { get; set; }
    }
}
