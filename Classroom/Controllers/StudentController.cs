using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Classroom.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using NUnit.Framework;
using WebGrease.Activities;


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
                var studentGroup = db.Student.Where(s => s.User.Equals(User.Identity.Name)).ToList();
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

        public ActionResult Create()
        {
            return View();
        }
#endregion

#region Create, Edit and Delete student
        [System.Web.Mvc.HttpPost]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Age,User")] Student student)
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
                student.User = user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (student.Id == 0)
            {
                student.Id = 1;
            }
            if (student.User.IsNullOrWhiteSpace())
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
            Subjects subjects = new Subjects();
            using (var context = new ClassroomContext())
            {
                try
                {
                    subjects = (from subs in context.Subject
                        where subs.StudentId == id
                        select subs).First();
                }
                catch (Exception ex)
                {
                    if (subjects.Id == 0)
                    {
                        return RedirectToAction("CreateMarks",new {StudentId=id});
                    }
                    ModelState.AddModelError("","Student has no marks to show.");

                    return RedirectToAction("Index"); ;
                }

            }
            if (subjects == null)
            {
                return HttpNotFound();
            }
            return View(subjects);                        
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
            Subjects subjects = new Subjects();
            using (var context = new ClassroomContext())
            {
                try
                {
                    subjects = (from subs in context.Subject
                                where subs.StudentId == id
                                select subs).First();
                    return View(subjects);
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
        public ActionResult EditMarks([Bind(Include = "Id,StudentId,English, Afrikaans,Math,NaturalScience,Geography,History,LifeOrientation")]Subjects subjects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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

        //POST:/Student/Create<arks/5
        [HttpPost]
        public ActionResult CreateMarks([Bind(Include = "Id,StudentId,English,Afrikaans,Math,NaturalScience,Geography,History,LifeOrientation")] Subjects subjects)
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
    }
}
