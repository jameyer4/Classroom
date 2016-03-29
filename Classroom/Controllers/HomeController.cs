using Classroom.Models;
using System.Web.Mvc;
using System.Linq;
using Classroom.Models.DB_Models;
using Classroom.Repository;
using System;
using System.Collections.Generic;

namespace Classroom.Controllers
{
    public class HomeController : Controller
    {
        private ClassroomContext db = new ClassroomContext();
        public ActionResult Index()
        {
            List<Notes> list = new List<Notes>();
            try
            {
                list = db.Notes.ToList();
                ViewBag.TeacherId = new GetTeachers().GetTeacherIdByUsername(User.Identity.Name);
            }
            catch (Exception ex) { }

            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AddNote()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNote([Bind(Include ="TeacherId,Title,Description,DateSet,DueDate")] Notes note)
        {
            string title = Request.Form["Title"];
            string description = Request.Form["Description"];
            var dateSet = DateTime.ParseExact(Request.Form["datepicker1"], "M/dd/yyyy", null);
            var dueDate = DateTime.ParseExact(Request.Form["datepicker2"], "M/dd/yyyy", null);
            var teacherId = new GetTeachers().GetTeacherIdByUsername(User.Identity.Name);
            try
            {
                note.Title = title;
                note.Description = description;
                note.DateSet = dateSet;
                note.DueDate = dueDate;
                note.TeacherId = teacherId;

                db.Notes.Add(note);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteNote(int id)
        {
            Notes note = db.Notes.Find(id);
            db.Notes.Remove(note);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}