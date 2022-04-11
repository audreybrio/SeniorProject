using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMultiTool.Backend.Models.AccessModel
{
    [Table("PERMISSIONS")]
    public partial class Permission
    {

        [Key]
        public int Permission_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PermissionDescription { get; set; }

        public Role Roles { get; set; }
    }
}
