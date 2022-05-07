


namespace StudentMultiTool.Backend.Models.RecoveryAccount
{
    public class RecoveryUserEmail
    {

        public string? username { get; set; }

        public string? email { get; set; }

        public bool EmailExist { get; set; }

        public bool UsernameExist { get; set; }

        public bool activate { get; set; }
    }
}
