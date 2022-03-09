using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentMultiTool.Backend.Services.Authentication.Model
{
    public class User
    {

        public string name;
        public string username;
        public string email;
        public string passcode;
        public string role;
        public string school;
        public bool active;


        public User(string n, string u, string e, string pass, string r, string s, bool a)
        {
            this.name = n;
            this.username = u;
            this.email = e;
            this.passcode = pass;
            this.role = r;
            this.school = s;
            this.active = a;


        }
        public string Name { get => name; set => name = value; }
        public string Username { get => username; set => username = value; }
        public string Role { get => role; set => role = value; }
        public bool Active { get => active; set => active = value; }
        public string Email { get => email; set => email = value; }
        public string Passcode { get => passcode; set => passcode = value; }

        public string School { get => school; set => school = value; }

        public string LoginErrorMsg { get; set; }
        public string Login2ErrorMsg { get; set; }
        public string Login3ErrorMsg { get; set; }
        public string Login4ErrorMsg { get; set; }
    }
}
