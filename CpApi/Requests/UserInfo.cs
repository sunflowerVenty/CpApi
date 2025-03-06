namespace CpApi.Requests
{
    public class UserInfo
    {
        public int id_User { get; set; }
        public bool isAdmin { get; set; }
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
