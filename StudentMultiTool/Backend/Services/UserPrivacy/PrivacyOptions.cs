namespace StudentMultiTool.Backend.Services.UserPrivacy
{
    public class PrivacyOptions
    {
        public string Username { get; set; } = string.Empty;
        public bool SellMyInfo { get; set; } = true;
        public PrivacyOptions() { }
        public PrivacyOptions(string username)
        {
            Username = username;
        }
        public PrivacyOptions(string username, bool SellMyInfo)
        {
            Username = username;
            this.SellMyInfo = SellMyInfo;
        }
    }
}
