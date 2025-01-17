using System.ComponentModel.DataAnnotations;

namespace CpApi.Model
{
    public class Users
    {
        [Key]
        public int id_User { get; set; }
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public bool Admin { get; set; }
    }
}
