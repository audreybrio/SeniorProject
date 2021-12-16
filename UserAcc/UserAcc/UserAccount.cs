using System;

namespace UserAcc
{
    public class UserAccount
    {
        public string name;
        public string username;
        public string password;
        public string email;
        public string passcode; 
        public string role;
        public bool active;
 

        public UserAccount(string n, string u, string p, string e, string pass, string r, bool a)
        {
            this.name = n;
            this.username = u;
            this.password = p;
            this.email = e;
            this.passcode = pass;
            this.role = r;
            this.active = a;


        }

        public string Name { get => name; set => name = value; }
        public string Username { get => username; set => username = value; }
        public string Role { get => role; set => role = value; }
        public bool Active { get => active; set => active = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Passcode { get => passcode; set => passcode = value; }

        static void Main(string[] args)
        {
        }
    }
}
