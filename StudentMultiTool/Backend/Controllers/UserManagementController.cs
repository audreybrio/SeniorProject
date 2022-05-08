using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using UserAcc;
using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Services.UserManagement;
using StudentMultiTool.Backend.Services.Authorization;
using StudentMultiTool.Backend.Services.Authorization.Entities;

namespace StudentMultiTool.Backend.Controllers
{
    [Authorize(Role.admin)]
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

        [HttpPost]
        [Route("bulkops")]
        public async Task<IActionResult> BulkOps(IFormFile file)
        {
            long size = file.Length;
            if (size > 0)
            {
                try
                {
                    var filePath = Path.Combine("../smt-storage/", DateTime.UtcNow.ToString(), file.Name);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    bool executed = false;

                    if (executed)
                    {
                        return StatusCode(200, "Bulk operation completed");
                    }
                    else
                    {
                        return StatusCode(500, "Could not complete bulk operation");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.GetType().FullName + "\n" + ex.Message);
                }
            }
            else
            {
                return StatusCode(415, "Empty file uploads are not supported");
            }
            return StatusCode(400, "Bad or malformed request; please try uploadin your file again");
        }
    }
}
