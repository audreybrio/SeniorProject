using System;

namespace UserAcc
{
    class UserAccount
    {
        private string name;
        private string username;
        private string password;
        private string email;
        private string passcode; 
        private string role;
        private bool active;
 

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
            UserAccount aud = new UserAccount("Audrey Brio", "abrio", "Pas$word1", "audrey.brio@student.csulb.edu", "1234", "Admin", true);
            Console.WriteLine(aud.name + " " + aud.username + " " + aud.password + " " + aud.email + " " + aud.passcode+ " " + aud.role + " "+ aud.active);
            aud.email = "michael.kriesel@student.csulb.edu";
            aud.username = "bncikle";
            Console.WriteLine(aud.name + " " + aud.username + " " + aud.password + " " + aud.email + " " + aud.passcode + " " + aud.role + " " + aud.active);
        }
    }
}
