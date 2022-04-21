using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("api/" + "comparison")]
    public class ScheduleComparisonController : Controller
    {
        // GET: ScheduleComparisonController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ScheduleComparisonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ScheduleComparisonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ScheduleComparisonController/Create
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

        // GET: ScheduleComparisonController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ScheduleComparisonController/Edit/5
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

        // GET: ScheduleComparisonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ScheduleComparisonController/Delete/5
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
