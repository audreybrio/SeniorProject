using StudentMultiTool.Backend.Services.DataAccess;
using StudentMultiTool.Backend.Services.UserPrivacy;

namespace StudentMultiTool.Backend.DAL
{
    public class PrivacyOptionsDAO
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Success { get; } = "Success";
        public PrivacyOptionsDAO()
        {
            ConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariableEnum.CONNECTIONSTRING);
        }
        public int InsertPrivacyOptions(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return 0;
            }
            SqlCommandRunner runner = new SqlCommandRunner(ConnectionString);
            runner.Query = "INSERT INTO UserPrivacyOptions (username, sellMyInfo) VALUES (@username, @sellMyInfo);";
            runner.AddParam("@sellMyInfo", true);
            runner.AddParam("@username", username);
            return runner.ExecuteNonQuery();
        }

        public PrivacyOptions SelectPrivacyOptions(string username)
        {
            PrivacyOptions options = new PrivacyOptions();
            if (string.IsNullOrEmpty(username))
            {
                return options;
            }
            options.Username = username;
            SqlCommandRunner runner = new SqlCommandRunner(ConnectionString);
            runner.Query = "SELECT * FROM UserPrivacyOptions WHERE username = @username;";
            runner.AddParam("@username", username);
            List<object[]> results = runner.ExecuteReader();
            if (results.Count > 0)
            {
                if (results[0].Length >= 2)
                {
                    try
                    {
                        options.SellMyInfo = (bool)results[0][1];
                    }
                    catch (Exception ex)
                    {
                        options.SellMyInfo = true;
                    }
                }
            }
            return options;
        }

        public int UpdatePrivacyOptions(PrivacyOptions options)
        {
            if (string.IsNullOrEmpty(options.Username))
            {
                return 0;
            }
            SqlCommandRunner runner = new SqlCommandRunner(ConnectionString);
            runner.Query = "UPDATE UserPrivacyOptions SET sellMyInfo = @sellMyInfo WHERE username = @username;";
            runner.AddParam("@sellMyInfo", options.SellMyInfo);
            runner.AddParam("@username", options.Username);
            return runner.ExecuteNonQuery();
        }

        public int DeletePrivacyOptions(PrivacyOptions options)
        {
            if (string.IsNullOrEmpty(options.Username))
            {
                return 0;
            }
            SqlCommandRunner runner = new SqlCommandRunner(ConnectionString);
            runner.Query = "DELETE FROM UserPrivacyOptions WHERE username = @username;";
            runner.AddParam("@sellMyInfo", options.SellMyInfo);
            runner.AddParam("@username", options.Username);
            return runner.ExecuteNonQuery();
        }
    }
}
