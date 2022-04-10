using System;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.AccessModel;

namespace StudentMultiTool.Backend.Services.AccessControl
{
    public class RBACController : Controller //MVC - controller class is responsible for processing and responding to browser requests
    {
         RBACContext db = new RBACContext();
        public IActionResult Index()
        {
            ViewBag.Message = db.RoleGet();
            return View();
        }



    }
}
