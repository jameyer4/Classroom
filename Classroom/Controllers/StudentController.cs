using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Classroom.Models;
using Microsoft.Ajax.Utilities;
using Classroom.Models.DB_Models;
using Classroom.Repository;
using System.Data.Entity;
using System.Net;

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
                GetStudents students = new GetStudents();
                var studentGroup = students.GetStudentsByTeacherUsername(User.Identity.Name);
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
        [HttpPost]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Age")] Student student)
        {
            ViewBag.userFlag = false;
            string lName = Request.Form.Get("LastName");
            string fName = Request.Form.Get("FirstName");
            int age = Convert.ToInt32(Request.Form.Get("Age"));
            try
            {                
                student.FirstName = fName;
                student.LastName = lName;
                student.Age = age;
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = "Cannot find Teacher.";
                return View();
            }
            //if (student.Id == 0)
            //{
            //    student.Id = 1;
            //}
            if (User.Identity.Name.IsNullOrWhiteSpace())
            {
                ViewBag.errorMessage = "Please login to create student.";
                ViewBag.userFlag = true;
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(GetStudent(student));
                if (student.ToString().IsNullOrWhiteSpace())
                {
                    throw new ArgumentException("Student values not complete");
                }
                db.SaveChanges();
                try
                {
                    CreateStudentMarks(student.Id);
                }
                catch (Exception ex)
                {
                    ViewBag.errorMessage = "Cannot Create Mark set for " + student.FirstName + ".";
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        private static Student GetStudent(Student student)
        {
            return student;
        }

       // GET: /Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
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
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(db.Students.Find(id));
            var marks = new GetMarks().GetMarksByStudentId(id);
            for (int i = 0; i < marks.Count; i++)
            {
                db.StudentMark.Remove(db.StudentMark.Find(marks[i].Id));
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Find student marks
        //GET: /Student/Marks/5
        public ActionResult Marks(int id)
        {
            GetSubjects getSubs = new GetSubjects();

            List<Subject> subjects = new List<Subject>();
            
            subjects = getSubs.GetSubjectsById(id);
            if (getSubs.GetSubjectsById(id).Count<1)
            {
                ModelState.AddModelError("", "Student has no marks to show.");
                return RedirectToAction("CreateMarks",new {StudentId=id});
            }

            ViewBag.SubList = subjects;
            return View();                        
        }
#endregion

//#region Edit marks
//        GET: /Student/EditMarks/5
//        public ActionResult EditMarks(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Subject subjects = new Subject();
//            using (var context = new ClassroomContext())
//            {
//                try
//                {
//                    subjects = (from subs in context.Subjects
//                                where subs.StudentId == id
//                                select subs).First();
//                    return View(subjects);
//                }
//                catch (Exception ex)
//                {
//                    ModelState.AddModelError("", "Student has no marks to show.");
//                    RedirectToAction("Index");
//                    return View();
//                }

//            }
//        }

//        POST: /Student/EditMarks/5
//        [HttpPost, ActionName("EditMarks")]
//        [ValidateAntiForgeryToken]
//        public ActionResult EditMarks([Bind(Include = "Id,Name,StudentId,TeacherId")]Subject subjects)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(subjects).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(subjects);
//        }
//#endregion

#region Create marks
        //GET: /Student/CreateMarks
        public ActionResult CreateMarks(int? id)
        {
            ViewBag.sId = id;
            return View();
        }

        //POST:/Student/Create<arks/5
        //[HttpPost]
        //public ActionResult CreateMarks([Bind(Include = "Id,Name")] Subject subject)
        //{

        //    GetStudents student = new GetStudents();
        //    ViewBag.userFlag = false;
        //    string name = Request.Form.Get("Name");
        //    try
        //    {
        //        subject.Name = name;
        //        subject.StudentId = student.GetStudentById().Id;
        //        subject.Age = age;
        //        subject.TeacherId = sTeacher.Id;
        //        //student.Teacher = sTeacher;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.errorMessage = "Cannot find Teacher.";
        //        return View();
        //    }
        //    if (student.Id == 0)
        //    {
        //        student.Id = 1;
        //    }
        //    if (teacher.GetTeacherById(student.TeacherId).UserName.IsNullOrWhiteSpace())
        //    {
        //        ViewBag.errorMessage = "Please login to create student.";
        //        ViewBag.userFlag = true;
        //        return View();
        //    }
        //    //student.User=

        //    if (ModelState.IsValid)
        //    {
        //        db.Students.Add(student);
        //        if (student.ToString().IsNullOrWhiteSpace())
        //        {
        //            throw new ArgumentException("Student values not complete");
        //        }
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    return View(student);
        //}
#endregion

        public ActionResult Class()
        {
            GetStudents students = new GetStudents();
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
                var studentGroup = students.GetStudentsByTeacherUsername(User.Identity.Name);
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
        public void CreateStudentMarks(int id)
        {
            
            GetTeachers teacher = new GetTeachers();
            for (int i = 1; i <= 7; i++)
            {
                StudentMark sMark = new StudentMark();
                sMark.SubjectId = i;
                sMark.StudentId = id;
                sMark.TeacherId = teacher.GetTeacherIdByUsername(User.Identity.Name);
                //Marks should be initialized as null
                if (sMark.Id == 0)
                {
                    sMark.Id = i;
                }
                if (ModelState.IsValid)
                {
                    db.StudentMark.Add(sMark);
                    if (sMark.ToString().IsNullOrWhiteSpace())
                    {
                        throw new ArgumentException("Student values not complete");
                    }
              //      db.SaveChanges();

                    //RedirectToAction("Index");
                }
            }
        }
    }
}
