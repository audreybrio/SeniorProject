using System;

namespace Marvel.UserAcc
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



    [Table("ROLES")]
    public partial class ROLE
    {
        public ROLE()
        {
            PERMISSIONS = new HashSet<PERMISSION>();
            UserAccounts = new HashSet<UserAccount>();
        }

        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
        public string RoleDetail { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsStudent { get; set; }
        public virtual ICollection<PERMISSION> PERMISSIONS { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }


    [Table("PERMISSIONS")]
    public partial class PERMISSION
    {
        public PERMISSION()
        {
            ROLES = new HashSet<ROLE>();
        }

        [Key]
        public int PermissionId { get; set; }

        [Required]
        [StringLength(50)]
        public string PermissionDetails { get; set; }

        public virtual ICollection<ROLE> ROLES { get; set; }
    }}
