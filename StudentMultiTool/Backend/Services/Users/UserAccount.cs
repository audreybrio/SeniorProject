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
    }

    public class ROLE
    {
        public int roleId;
        public string roleName;
        public string roleDetail;
        public bool isAdmin;
        public bool isStudent;

        public ROLE(int ri, string rn, string rd, bool a, bool s)
        {
            this.roleId = ri;
            this.RoleName = rn;
            this.RoleDetail = rd;
            this.isAdmin = a;
            this.isStudent = s;

            PERMISSION = new HashSet<PERMISSION>();
            UserAccount = new HashSet<UserAccount>();
        }
        public int RoleId { get => roleId; set => roleId = value; }
        public string RoleName { get => roleName; set => roleName = value; }
        public string RoleDetail { get => roleDetail; set => roleDetail = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsStudent { get => isStudent; set => isStudent = value; }
        public virtual ICollection<PERMISSION> PERMISSION { get; set; }
        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
    public class PERMISSION
    {
        public int permissionId;
        public string permissionDetails;

        public PERMISSION(int pr, string pd)
        {
            this.PermissionId = pr;
            this.PermissionDetails = pd;
            ROLE = new HashSet<ROLE>();
        }

        public int PermissionId {get => permissionId; set => permissionId = value;}
        public string PermissionDetails { get => permissionDetails; set => permissionDetails = value; }

        public virtual ICollection<ROLE> ROLE { get; set; }
    }
}
