using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marvel.RBAC.AccessDenied.Controllers
{
    public class AccessDeniedController : Controller
    {
        // GET: Unauthorised
        public IActionResult Denied()
        {
            Session.Abandon();
            return View();
        }
    }
}