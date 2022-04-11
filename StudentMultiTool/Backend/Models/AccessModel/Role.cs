using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentMultiTool.Backend.Models.AccessModel
{
    [Table("ROLES")]
    public class Role
    {
        [Key]
        public int Role_Id { get; set; }

        [Required]
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public bool IsSysAdmin { get; set; }

        public List<Permission> Permissions { get; set; }
        //public List<UserAccount> Users { get; set; }


    }
}
