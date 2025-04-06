﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CpApi.Model
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        [Required]
        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"));
 
        public string? ImageUrl { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Users Sender { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Users Receiver { get; set; }
    }
}