using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMultiTool.Backend.Services.AidEligibility
{

    public class AidController : Controller
    {
        // GET: AidController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AidController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AidController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AidController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AidController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AidController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AidController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AidController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
