namespace StudentMultiTool.Backend.Services.Authentication
{
    public class Validate
    {

        public bool ValidatePasscode(string passcode)
        {
            string[] invalid = { "#", "$", "%", "^", "&", "*", "(", ")", "{", "}", "[", "]", "|", ";", ":", "'", "<", ">", "=", "+", "-", "_", "~", "`" };
            List<string> invalidComponents = new List<string>(invalid);
            int passLegth = passcode.Length;
            if (passLegth < 7)
            {
                return false;   
            }

            else
            {
                foreach (string i in invalidComponents)
                {
                    if (passcode.Contains(i))
                    {
                        return false;
                    }
                }

                return true;
            }
        }



        public bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
