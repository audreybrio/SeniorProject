using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.DAL;
using UserAcc;

namespace StudentMultiTool.Backend.Services.UserManagement
{
    public class UserManager
    {
        public string Success { get; } = "Success";
        public List<UserAccount> GetUsers()
        {
            UserAccountDAO uad = new UserAccountDAO();
            List<UserAccount> results = new List<UserAccount>();
            try
            {
                results = uad.SelectAll();
            }
            catch (Exception ex)
            {
                UserAccount error = new UserAccount();
                error.Id = -1;
                error.name = ex.Message;
                if (!string.IsNullOrEmpty(ex.GetType().FullName))
                {
                    error.username = ex.GetType().FullName;
                }
                results.Add(error);
            }
            return results;
        }

        public List<string> GetRoles()
        {
            //return new List<string> { "admin", "student" };
            RoleDAO rd = new RoleDAO();
            List<string> results = rd.SelectAllRoles();
            return results;
        }

        public string CreateUser(UserAccount user)
        {
            if (user != null)
            {
                UserAccountDAO uad = new UserAccountDAO();
                string result = uad.InsertUser(user);
                if (result.Equals(uad.Success))
                {
                    return this.Success;
                }
            }
            return "user cannot be null";
        }

        public string UpdateUsers(List<UserAccount> users)
        {
            if (users.Count > 0)
            {
                UserAccountDAO uad = new UserAccountDAO();
                int updated = 0;
                int errors = 0;
                foreach (UserAccount user in users)
                {
                    try
                    {
                        string currentResult = uad.UpdateSingle(user);
                        if (currentResult.Equals(uad.Success))
                        {
                            updated++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errors++;
                    }
                }
                if (updated == 0)
                {
                    return "Could not update users, with " + errors.ToString() + " errors, out of " + users.Count + " users";
                }
                else
                {
                    return Success;
                }
            }
            return "Bad or malformed request; received " + users.Count + " users";
        }

        public string DeleteUsers(List<UserAccount> users)
        {
            if (users.Count > 0)
            {
                UserAccountDAO uad = new UserAccountDAO();
                int deleted = 0;
                int errors = 0;
                foreach (UserAccount user in users)
                {
                    try
                    {
                        string currentResult = uad.DeleteSingle(user);
                        if (currentResult.Equals(uad.Success))
                        {
                            deleted++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errors++;
                    }
                }
                if (deleted == 0)
                {
                    return "Could not update users, with " + errors.ToString() + " errors, out of " + users.Count + " users";
                }
                else
                {
                    return Success;
                }
            }
            return "Bad or malformed request; received " + users.Count + " users";
        }

        public async Task<string> BulkOps(IFormFile file)
        {
            long size = file.Length;
            if (size > 0)
            {
                try
                {
                    string year = DateTime.UtcNow.Year.ToString();
                    string month = DateTime.UtcNow.Month.ToString();
                    string day = DateTime.UtcNow.Day.ToString();
                    string hour = DateTime.UtcNow.Hour.ToString();
                    string minute = DateTime.UtcNow.Minute.ToString();
                    string second = DateTime.UtcNow.Second.ToString();
                    string name = "bulkop-" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + ".txt";
                    var filePath = Path.Combine("../smt-storage/", name);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    bool executed = false;

                    BulkOperationInterpreter interpreter = new BulkOperationInterpreter();
                    string result = interpreter.Execute(filePath);
                    executed = result.Equals(interpreter.Success);

                    if (executed)
                    {
                        return Success;
                    }
                    else
                    {
                        return "Could not complete bulk operation";
                    }
                }
                catch (Exception ex)
                {
                    return ex.GetType().FullName + "\n" + ex.Message;
                }
            }
            else if (size >= 2147483647)
            {
                return "Maximum supported file size is 2 GB";
            }
            else
            {
                return "Empty file uploads are not supported";
            }
            return "Bad or malformed request; please try uploading your file again";
        }

        public bool SearchUsers(string username)
        {
            List<UserAccount> users = this.GetUsers();
            foreach (UserAccount user in users)
            {
                if (user.Username.Equals(username))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
