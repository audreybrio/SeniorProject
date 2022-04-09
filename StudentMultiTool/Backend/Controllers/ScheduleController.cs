using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Models.ScheduleBuilder;
using StudentMultiTool.Backend.Services.ScheduleBuilder;
using System.Data;
using System.Data.SqlClient;

namespace StudentMultiTool.Backend.Controllers
{
    [ApiController]
    [Route("schedule")]
    public class ScheduleController : Controller
    {
        [HttpGet("getlist/{username}")]
        public IEnumerable<Schedule> GetList(string username)
        {
            // TODO: check that the user is authenticated
            // TODO: convert WL's to logs
            Console.WriteLine("Firing ScheduleController.GetList");
            // Get user hash so we know who to get schedules for
            string? userHash = null;
            Console.WriteLine("Getting user hash for user \"" + username + "\"");
            ScheduleListBuilder builder = new ScheduleListBuilder();
            userHash = builder.GetUserHash(username);
            if (userHash == null)
            {
                Console.WriteLine("userHash was null");
                return Enumerable.Empty<Schedule>();
            }

            // Get all schedules the user owns (and/or can collaborate on)
            IEnumerable<Schedule> list = builder.GetAllSchedulesForUser(userHash).Result;
            Console.WriteLine("Request finished");
            if (list == null)
            {
                return Enumerable.Empty<Schedule>();
            }
            return list;
        }
        [HttpPost]
        [HttpPost("newschedule/{title}/{username}")]
        public string NewSchedule(string title, string username)
        {
            // TODO: check that the user is authenticated
            // TODO: convert WL's to logs
            Console.WriteLine("Firing ScheduleController.NewSchedule with args: " + title + ", " + username);
            int rowsAffected = 0;
            string? userHash = null;

            Console.WriteLine("Getting user hash for user \"" + username + "\"");
            ScheduleListBuilder builder = new ScheduleListBuilder();
            userHash = builder.GetUserHash(username);
            if (userHash == null)
            {
                return "Could not get userHash";
            }

            // Add schedule to DB
            Console.WriteLine("Add schedule to db");
            Schedule newSchedule = new Schedule(
               -1,
               DateTime.Now,
               DateTime.Now,
               title,
               userHash + "\\" + title + ".json"
            );
            ScheduleManager manager = new ScheduleManager();
            int? newId = manager.InsertSchedule(newSchedule);

            if (newId != null)
            {
                // Add the owner as a collaborator to the DB
                Console.WriteLine("Add the owner as a collaborator to the db");
                rowsAffected = manager.InsertCollaborator((int) newId, userHash, true, true);
            }

            if (rowsAffected > 0 )
            {
                return "Success";
            }
            return "Oops";
        }

        //[HttpPost("schedule/newitem/")]
        //public string NewItem

        //// GET: ScheduleSelectionController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: ScheduleSelectionController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ScheduleSelectionController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ScheduleSelectionController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ScheduleSelectionController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ScheduleSelectionController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ScheduleSelectionController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ScheduleSelectionController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
