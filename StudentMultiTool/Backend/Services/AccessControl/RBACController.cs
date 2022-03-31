using System;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.AccessModel;

namespace StudentMultiTool.Backend.Services.AccessControl
{
    public class RBACController : Controller //MVC - controller class is responsible for processing and responding to browser requests
    {
         RBACContext db;
        public IActionResult Index()
        {
            return View(db.Users.Where(r=> r.active == false || r.active == null).OrderBy(r => r.username).ThenBy(r => r.role).ToList());
        }



    }
}
