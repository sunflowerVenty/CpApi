using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CpApi.Model
{
    public class Messages
    {
        [Key]
        public int Id_Message {  get; set; }

        public string Message { get; set; }
        public DateTime dateTimeSent {get; set; }
        [Required]
        [ForeignKey("Films")]
        public int Film_Id {  get; set; }
        public Films Films { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int User_Id { get; set; }
        public Users Users { get; set; }

        public string ImageURL { get; set; }

        [Required]
        [ForeignKey("Recipients")]
        public int Recipient_Id { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Users Recipients { get; set; }
    }
}
