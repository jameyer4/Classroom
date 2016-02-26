using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Classroom.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace Classroom.Controllers
{
    //[Authorize(Roles="User")]
    public class StudentController : Controller
    {
        private ClassroomContext db = new ClassroomContext();
        Student childStudent = new Student(); 
        List<Student> studentList = new List<Student>();

#region Constructors and Index
        public StudentController()
        {
            db.Database.Log = l => Debug.Write(l);
        }

        public StudentController(Student student)
        {
            childStudent = student;
        }

        public StudentController(List<Student> students)
        {
            studentList = students;
        }

        [System.Web.Http.Route("Index")]
        public ViewResult Index()
        {
            ViewBag.userFlag = false;
            ViewBag.errorFlag = true;
            ViewBag.errorMessage = "Go ahead and create a student.";

            if (User.Identity.Name.IsNullOrWhiteSpace())
            {
                ViewBag.userFlag = true;
                ViewBag.errorMessage = "Please login to access student information";
                return View();
            }
            try
            {
                var studentGroup = db.Student.Where(s => s.Teacher.UserName.Equals(User.Identity.Name)).ToList();
                if (studentGroup.Count > 0)
                {
                    ViewBag.errorFlag = false; 
                }
                return View(studentGroup);
            }
            catch (Exception ex)
            {
                //Create log for errors
                return View();
            }
            
            
        }

#endregion

#region Create, Edit and Delete student
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Age,Teacher")] Student student)
        {
            ViewBag.userFlag = false;
            string lName = Request.Form.Get("LastNameInput");
            string fName = Request.Form.Get("FirstNameInput");
            int age = Convert.ToInt32(Request.Form.Get("AgeInput"));
            try
            {
                string user = User.Identity.GetUserName();
                student.FirstName = fName;
                student.LastName = lName;
                student.Age = age;
                student.TeacherId = db.Teacher.Single(t => t.UserName.Equals(user)).Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (student.Id == 0)
            {
                student.Id = 1;
            }
            if (student.Teacher.UserName.IsNullOrWhiteSpace())
            {
                ViewBag.errorMessage = "Please login to create student.";
                ViewBag.userFlag = true;
                return View();
            }  
            //student.User=

            if (ModelState.IsValid)
            {
                db.Student.Add(student);
                if (student.ToString().IsNullOrWhiteSpace())
                {
                    throw new ArgumentException("Student values not complete");
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(student);
        }

        //GET: /Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Age")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }


        //GET: /Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Student student = db.Student.Find(id);
            db.Student.Remove(db.Student.Find(id));
            db.SaveChanges();   
            return RedirectToAction("Index");
        }
#endregion

#region Find student marks
        //GET: /Student/Marks/5

        public ActionResult Marks(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Subject> studSubs = null;
            using (var context = new ClassroomContext())
            {
                try
                {
                    studSubs = db.Subject.Where(s => s.Id.Equals(id)).ToList();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Student has no marks to show.");
                    if (studSubs==null)
                    {
                        return RedirectToAction("CreateMarks",new {StudentId=id});
                    }

                    return RedirectToAction("Index"); ;
                }

            }

            return View(studSubs);                        
        }
#endregion

#region Edit marks
        //GET: /Student/EditMarks/5
        public ActionResult EditMarks(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new ClassroomContext())
            {
                try
                {
                    List<Subject> studSubs = db.Subject.Where(s => s.Id.Equals(id)).ToList();
                    return View(studSubs);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Student has no marks to show.");
                    RedirectToAction("Index");
                    return View();
                }

            }
        }

        //POST: /Student/EditMarks/5
        [HttpPost, ActionName("EditMarks")]
        [ValidateAntiForgeryToken]
        public ActionResult EditMarks([Bind(Include = "Id,StudentId,Name,Mark,TeacherId,ClassId")]Subject subjects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //return subjects;
            return View(subjects);
        }

        #endregion

#region Create marks
        //GET: /Student/CreateMarks
        public ActionResult CreateMarks(int? id)
            {
                ViewBag.sId = id;
                return View();
        }

        //POST:/Student/CreateMarks/5
        [HttpPost]
        public ActionResult CreateMarks([Bind(Include = "Id,StudentId,Name,Mark,TeacherId,ClassId")] Subject subjects)
        {
            
            if (ModelState.IsValid)
            {
                db.Subject.Add(subjects);
                if (subjects.ToString().IsNullOrWhiteSpace())
                {
                    throw new ArgumentException("Student values not complete");
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(subjects);
        }
#endregion

        public ActionResult Class()
        {
            ViewBag.userFlag = false;
            ViewBag.errorFlag = true;
            ViewBag.errorMessage = "Go ahead and create a student.";

            if (User.Identity.Name.IsNullOrWhiteSpace())
            {
                ViewBag.userFlag = true;
                ViewBag.errorMessage = "Please login to access student information";
                return View();
            }
            try
            {
                var studentGroup = db.Student.Where(s => s.Teacher.UserName.Equals(User.Identity.Name)).ToList();
                if (studentGroup.Count > 0)
                {
                    ViewBag.errorFlag = false;
                }
                return View(studentGroup);
            }
            catch (Exception ex)
            {
                //Create log for errors
                return View();
            }
        }
    }
}
