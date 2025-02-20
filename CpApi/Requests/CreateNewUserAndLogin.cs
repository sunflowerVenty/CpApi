namespace CpApi.Requests
{
    public class CreateNewUserAndLogin
    {
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public bool Admin { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
