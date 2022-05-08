using StudentMultiTool.Backend.Services.Authorization.Entities;

namespace StudentMultiTool.Backend.Models.Users
{
    public class AuthenticateResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }

        public Role Role { get; set; }

        public string token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            id = user.id;
            name = user.name;
            email = user.email;
            username = user.username;
            Role = user.Role;
            token = token;
        }
    }
}
