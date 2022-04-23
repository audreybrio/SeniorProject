using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using StudentMultiTool.Backend.Services.DataAccess;

namespace StudentMultiTool.Backend.Services.AidEligibility
{
    [ApiController]
    [Route("api/aid")]
    public class AidController : ControllerBase
    {
        // GET: AidController
        public IActionResult SelectScholarships()
        {
            SQLServerDAO sqlServerDAO = new SQLServerDAO();


            return Ok();
        }

        // GET: AidController/Details/5
        public IActionResult Details(int id)
        {
            return Ok();
        }

        // GET: AidController/Create
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: AidController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }

        // GET: AidController/Edit/5
        public IActionResult Edit(int id)
        {
            return Ok();
        }

        // POST: AidController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }

        // GET: AidController/Delete/5
        public IActionResult Delete(int id)
        {
            return Ok();
        }

        // POST: AidController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }
    }
}
