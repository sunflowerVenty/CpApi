namespace CpApi.Requests
{
    public class UpdateUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
    }
}
