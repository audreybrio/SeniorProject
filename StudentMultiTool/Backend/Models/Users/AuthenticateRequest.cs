using System.ComponentModel.DataAnnotations;

namespace StudentMultiTool.Backend.Models.Users
{
    public class AuthenticateRequest
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
