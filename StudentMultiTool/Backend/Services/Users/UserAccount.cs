using System;

namespace UserAcc
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string name;
        public string username;
        public string password;
        public string email;
        public string passcode; 
        public string role;
        public bool active;
        public string school;
 
        public UserAccount()
        {
            Name = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Passcode = string.Empty;
            Email = string.Empty;
            Role = "student";
            Active = false;
            School = string.Empty;
        }
        public UserAccount(string email, string password, string userName, string school)
        {
            this.email = email;
            this.name = "";
            this.username = userName;
            this.password = password;
            this.passcode = "";
            this.role = "student";
            this.active = true;
            this.school = school;

        }

        public string Name { get => name; set => name = value; }
        public string Username { get => username; set => username = value; }
        public string Role { get => role; set => role = value; }
        public bool Active { get => active; set => active = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Passcode { get => passcode; set => passcode = value; }
        public string School { get => school; set => school = value; }
    }

    
}
