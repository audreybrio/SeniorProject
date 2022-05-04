using Microsoft.AspNetCore.Mvc;
using UserAcc;
using StudentMultiTool.Backend.DAL;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/usermanagement")]
    public class UserManagementController : Controller
    {
        [HttpGet]
        [Route("getusers")]
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

        [HttpGet]
        [Route("getRoles")]
        public List<string> GetRoles()
        {
            //return new List<string> { "admin", "student" };
            RoleDAO rd = new RoleDAO();
            List<string> results = rd.SelectAllRoles();
            return results;
        }

        [HttpPost]
        [Route("updateUsers")]
        public ActionResult UpdateUsers([FromBody] List<UserAccount> users)
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
                    return StatusCode(500, "Could not update users, with " + errors.ToString() + " errors, out of " + users.Count + " users");
                }
                else
                {
                    return StatusCode(200, "Updated " + updated.ToString() + " users with " + errors.ToString() + " errors, out of " + users.Count + " users");
                }
            }
            return StatusCode(400, "Bad or malformed request; received " + users.Count + " users");
        }

        [HttpPost]
        [Route("deleteUsers")]
        public ActionResult DeleteUsers([FromBody] List<UserAccount> users)
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
                    return StatusCode(500, "Could not update users, with " + errors.ToString() + " errors, out of " + users.Count + " users");
                }
                else
                {
                    return StatusCode(200, "Updated " + deleted.ToString() + " users with " + errors.ToString() + " errors, out of " + users.Count + " users");
                }
            }
            return StatusCode(400, "Bad or malformed request; received " + users.Count + " users");
        }
    }
}
