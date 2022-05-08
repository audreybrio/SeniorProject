namespace StudentMultiTool.Backend.Services.Authorization.Entities
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }

        public string passcode { get; set; }
        public Role Role { get; set; }
    }
}
