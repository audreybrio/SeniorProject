using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentMultiTool.Backend.Services.Authentication.Model
{
    public class User
    {
        [Required(ErrorMessage = "This Field is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Passcode { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password)]
        public string OTP { get; set; }

        public string LoginErrorMsg { get; set; }
        public string Login2ErrorMsg { get; set; }
        public string Login3ErrorMsg { get; set; }
        public string Login4ErrorMsg { get; set; }
    }
}
