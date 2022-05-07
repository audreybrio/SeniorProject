namespace StudentMultiTool.Backend.Models.Registration
{
    public class Registration
    {
        public string Passcode { get; set; } = "";
        public string Email { get; set; } = "";
        public string University { get; set; } = "";
        public string Token { get; set; } = "";
        public bool ValidPasscode { get; set; }
        public bool ValidEmail { get; set; }
        public bool ValidUniversity { get; set; }
        public bool EmailExist { get; set; }
    }
}
