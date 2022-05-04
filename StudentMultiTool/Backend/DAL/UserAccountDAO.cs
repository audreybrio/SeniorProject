using System.Data.SqlClient;
using StudentMultiTool.Backend.Services.DataAccess;
using UserAcc;

namespace StudentMultiTool.Backend.DAL
{
    public class UserAccountDAO
    {
        public string ConnectionString { get; set; } = string.Empty;
        public SqlCommandRunner Runner { get; set; }
        public string Success { get; } = "Success";
        public UserAccountDAO()
        {
            ConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariableEnum.CONNECTIONSTRING);
        }
        public string UpdateSingle(UserAccount user)
        {
            SqlCommandRunner runner = new SqlCommandRunner(ConnectionString);
            string query = "UPDATE UserAccounts SET ";
            if (!string.IsNullOrEmpty(user.Username))
            {
                query += "username = @username, ";
                runner.UpdateQuery(query);
                runner.AddParam("@username", user.Username);
            }
            if (!string.IsNullOrEmpty(user.Name))
            {
                query += "name = @name, ";
                runner.UpdateQuery(query);
                runner.AddParam("@name", user.Name);
            }
            if (!string.IsNullOrEmpty(user.Email))
            {
                query += "email = @email, ";
                runner.UpdateQuery(query);
                runner.AddParam("@email", user.Email);
            }
            if (!string.IsNullOrEmpty(user.Role))
            {
                query += "role = @role, ";
                runner.UpdateQuery(query);
                runner.AddParam("@role", user.Role);
            }
            query += "active_status = @active_status ";
            query += "WHERE id = @id;";
            runner.UpdateQuery(query);
            runner.AddParam("@active_status", user.Active);
            runner.AddParam("@id", user.Id);
            int rowsAffected = runner.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                return "Could not update user with ID " + user.Id.ToString();
            }
            if (rowsAffected > 1)
            {
                return rowsAffected.ToString() + " rows updated, please check the database";
            }
            return Success;
        }
        public string DeleteSingle(UserAccount user)
        {
            SqlCommandRunner runner = new SqlCommandRunner(ConnectionString);
            string query = "DELETE FROM UserAccounts WHERE id = @id;";
            runner.Query = query;
            runner.AddParam("@id", user.Id);
            int rowsAffected = runner.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                return "Could not delete user with ID " + user.Id.ToString();
            }
            if (rowsAffected > 1)
            {
                return rowsAffected.ToString() + " rows deleted, please check the database";
            }
            return Success;
        }
        public UserAccount? SelectSingle(string username)
        {
            SqlCommandRunner Runner = new SqlCommandRunner(ConnectionString);
            Runner.Query = "SELECT * FROM UserAccounts WHERE username = @username";
            Runner.AddParam("@username", username);
            List<object[]> results = Runner.ExecuteReader();
            if (results.Count > 0)
            {
                UserAccount user = this.UnpackQueryResult(results[0]);
            }
            return null;
        }
        public List<UserAccount> SelectAll()
        {
            SqlCommandRunner Runner = new SqlCommandRunner(ConnectionString);
            Runner.Query = "SELECT * FROM UserAccounts";
            List<object[]> queryResults = Runner.ExecuteReader();
            List<UserAccount> result = new List<UserAccount>();
            foreach (object[] row in queryResults)
            {
                if (row != null)
                {
                    UserAccount current = this.UnpackQueryResult(row);
                    if (current != null)
                    {
                        result.Add(current);
                    }
                }
            }
            if (result.Count == 0)
            {
                UserAccount errorUser = new UserAccount();
                errorUser.Id = -1;
                errorUser.name = "Error";
                errorUser.username = "Could not get users";
                result.Add(errorUser);
            }
            return result;
        }
        private UserAccount UnpackQueryResult(object[] results)
        {
            UserAccount user = new UserAccount();
            int? id = null;
            string? name = string.Empty;
            string? username = string.Empty;
            string? email = string.Empty;
            string? role = string.Empty;
            string? school = string.Empty;
            bool? active = null;

            try
            {
                id = (int)results[0];
                name = (string)results[1];
                username = (string)results[2];
                email = (string)results[3];
                // skipping passcode since we don't want to reveal the hash!
                role = (string)results[5];
                school = (string)results[6];
                active = (bool)results[7];
            }
            catch (Exception)
            {
                // We just want to catch index out of bounds exceptions here
                // If there's no data there's no data
            }
            if (id != null)
            {
                user.Id = (int)id;
            }
            if (!string.IsNullOrEmpty(name))
            {
                user.Name = name;
            }
            if (!string.IsNullOrEmpty(username))
            {
                user.Username = username;
            }
            if (!string.IsNullOrEmpty(email))
            {
                user.Email = email;
            }
            if (!string.IsNullOrEmpty(role))
            {
                user.Role = role;
            }
            if (!string.IsNullOrEmpty(school))
            {
                user.school = school;
            }
            if (active != null)
            {
                user.Active = (bool)active;
            }
            else
            {
                user.Active = false;
            }
            user.passcode = "null";
            return user;
        }
    }
}
