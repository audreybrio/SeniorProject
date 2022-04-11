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
            ViewBag.Message = db.GetAllRole();
            return View();
        }

        [HttpGet]
        public IActionResult AddNewRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRole(Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(db.AddRole(role))
                    {
                        ViewBag.Message = "Role details added successfully";
                    }
                }
            }
            catch 
            {
                return View();
            }

            return View();
        }


        [HttpGet]
        public IActionResult EditRoleDetails(int id)
        {
            return View(db.GetAllRole().Find(r => r.Role_Id == id));

        }

        [HttpPost]
        public IActionResult EditRoleDetails(int id, Role role)
        {
            try
            {
                db.UpdateRole(role);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult DeleteRoleDetails(int id)
        {
            try
            {
                if (db.DeleteRole(id))
                {
                    ViewBag.AlertMsg = "Role details deleted successfully";

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();

            }
        }



    }
}
