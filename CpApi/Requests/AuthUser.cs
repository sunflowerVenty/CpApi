using System.ComponentModel.DataAnnotations;

namespace CpApi.Requests
{
    public class AuthUser
    {
        [Required]
        public string Email { get; set; }

        [Required]

        public string Password { get; set; }
    }
}
