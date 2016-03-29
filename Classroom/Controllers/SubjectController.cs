using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Classroom.Models;
using Classroom.Models.DB_Models;
using Classroom.Repository;
using System;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Net;

namespace Classroom.Controllers
{
    // [Authorize(Roles = "User")]
    public class SubjectController : Controller
    {
        private ClassroomContext db = new ClassroomContext();
        List<Subject> subjectList = new List<Subject>();

        #region Constructors and Index
        public SubjectController()
        {
        }

        public SubjectController(List<Subject> subjects)
        {
            subjectList = subjects;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Error = true;
            try
            {
                var teacherId = new GetTeachers().GetTeacherIdByUsername(User.Identity.Name);
                var subjects = new GetSubjects().GetSubjectsByTeacherId(teacherId);
                var tasks = new GetTeacherTasks().GetAllTeacherTasks(teacherId);
                ViewBag.mySubjects = subjects;
                ViewBag.myTasks = tasks;
                ViewBag.Error = false;
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = "Log in to see your subjects.";

            }
            //return RedirectToAction("ChartMain"); --Uncomment to see average chart
            return View();
        }

        #endregion

        //#region ChartMain
        //public ActionResult ChartMain()
        //{
        //    ViewBag.DataFlag = false;
        //    if (User.Identity.Name.IsNullOrWhiteSpace())
        //    {
        //        ViewBag.DataFlag = true;
        //    }
        //    Subject subjects = new Subject();
        //    //Needs optimisation
        //    //Check if the student has 

        //    bool studMarks = db.Subjects.Any(s => s.Mark > 0 && db.Teacher.Select(x => x.UserName.Equals(User.Identity.Name) && x.Id.Equals(s.Student.Id)).Distinct().Count() > 1);
        //    ViewBag.hasMarks = studMarks;
        //    return View();
        //}

        //public ActionResult Chart()
        //{
        //    var subjects = db.Subjects.ToList();
        //    var students = db.Students.Where(s => s.TeacherId.Equals(db.Teacher.Select(t=>t.UserName.Equals(User.Identity.Name)))).ToList();
        //    ViewBag.errorMessage = "No student subject data to display. ";
        //    ViewBag.dataFlag = false;

        //    List<double> averages = new List<double>();
        //    List<string> names = new List<string>();
        //    int count = 0;
        //    bool flag = false;
        //    foreach (var x in subjects)
        //    {
        //        try
        //        {
        //            var studId = students[count].Id;
        //            if (x.Student.Id.Equals(studId))
        //            {
        //                var classSubj = db.Subjects.Where(s => s.Class.Id.Equals(studId)).ToList();
        //                double mark = 0, sum = 0;
        //                for (int i = 0; i < classSubj.Count; i++)
        //                {
        //                    mark = classSubj[i].Mark;
        //                    sum += mark;
        //                }
        //                averages.Add(sum / 7);
        //                var stud = db.Students.Find(x.Student.Id);
        //                names.Add(stud.FirstName + " " + stud.LastName);
        //            }
        //            count++;
        //        }
        //        catch (Exception ex)
        //        {
        //            //log issue or get better handling for no student ID error
        //            flag = true;
        //            break;
        //        }
        //    }

        //    ViewBag.Averages = averages;
        //    ViewBag.Names = names;

        //    if (names.Count < 1 || flag)
        //    {
        //        ViewBag.dataFlag = true;//There is no data to display
        //        return View();
        //    }

        //    var chart = new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla)
        //        .AddTitle("Student Averages")
        //        .AddSeries(name: "Student marks", xValue: names, yValues: averages)
        //        .Write();

        //    return View();
        //}
        [HttpGet]
        public ActionResult AssignSubjects()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AssignSubjects([Bind(Include = "Id,Name")] Subject subject)
        {
            string subjectName = Request.Form["sDropdown"];
            var mySubject = new GetSubjects().GetSubjectByName(subjectName);
            var teacher = new GetTeachers().GetTeacherByUsername(User.Identity.Name);

            TeacherSubjects ts = new TeacherSubjects();
            ts.SubjectId = mySubject.Id;
            ts.TeacherId = teacher.Id;
            if(new GetTeacherSubjects().GetTeacherSubjectByTeacherIdAndSubjectId(ts.TeacherId, ts.SubjectId)== null)
            {
                db.TeacherSubjects.Add(ts);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        //#endregion
        [HttpGet]
        [System.Web.Http.Route("Subject/SubjectView/{subject}")]
        public ActionResult SubjectView(string subject)
        {
            ViewBag.Subject = subject;
            var tasks = db.Tasks.ToList();
            return View(tasks);
        }
        public ActionResult AddTask(int id)
        {
                ViewBag.SubjectName = new GetSubjects().GetSubjectById(id).Name;
 
            return View();
        }
        [HttpPost]
        public ActionResult AddTask([Bind(Include = "Id,DateGiven,SubmissionDate,TaskName,Description,TeacherId,SubjectId")] Tasks Task)
        {
            NameValueCollection nvc = Request.Form;
            var TeacherId = new GetTeachers().GetTeacherIdByUsername(User.Identity.Name);// Remove?
            var mySubject = new GetSubjects().GetSubjectByName(Request.Form.Get("sname"));
            var teacher = new GetTeachers().GetTeacherByUsername(User.Identity.Name);
            //Use object injected!
            Tasks ts = new Tasks();
            ts.SubjectId = mySubject.Id;
            ts.TeacherId = teacher.Id;
            var d = Request.Form.Get("datepicker1");
            var date = DateTime.ParseExact(Request.Form.Get("datepicker1"),"M/dd/yyyy", null);
            ts.DateGiven = Convert.ToDateTime(date);
            date = DateTime.ParseExact(Request.Form.Get("datepicker2"), "M/dd/yyyy", null);
            ts.SubmissionDate = Convert.ToDateTime(date);
            ts.TaskName = Request.Form.Get("TaskName");
            ts.Description = Request.Form.Get("Description");
            
            //if (ts.Id == 0)
            //{
            //    ts.Id = 1;
            //}
            db.Tasks.Add(ts);
            db.SaveChanges();
            var students = new GetStudents().GetStudentsByTeacherUsername(User.Identity.Name);
            foreach(var item in students)
            {
                StudentTasks st = new StudentTasks();
                st.StudentId = item.Id;
                st.TaskId = ts.Id;
                st.Id = (st.Id==0) ? 1 : st.Id;
                db.StudentTasks.Add(st);
                db.SaveChanges();
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }
        public ActionResult StudentTasks(int? id)
        {
            //Insert TRY BLOCK to improve exception handling
            string[] sid = Request.Url.ToString().Split('/');
            int nid = Convert.ToInt32(sid[sid.Length - 1]);//Get task ID from url
            List<StudentTasks> sTasks = new GetStudentTasks().GetStudentTasksByTasksId(nid);
            List<Student> students = new List<Student>();
            GetStudents student = new GetStudents();
            Tasks tasks = new Tasks();
            try
            {
                tasks = new GetTeacherTasks().GetTasksById(sTasks[0].TaskId);
            }
            catch(Exception ex)
            {
                //
            }

            foreach (var item in sTasks)
            {
                var x = student.GetStudentById(item.StudentId);
                students.Add(x);
            }
            ViewBag.sTask = sTasks;
            ViewBag.Students = students;
            ViewBag.Task = tasks;
            return View(sTasks);
        }
        [HttpPost]
        public ActionResult StudentTasks(List<StudentTasks> sTasks)
        {
            
            return View();
        }
        // GET: /Student/Edit/5
        public ActionResult EditMark(int? id)
        {
            int nid = id ?? 0;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentTasks sTask = db.StudentTasks.Find(id);
            if (sTask == null)
            {
                return HttpNotFound();
            }
            var student = new GetStudents().GetStudentByStudentTaskId(nid);
            ViewBag.student = student;
            return View(sTask);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditMark([Bind(Include = "Id,StudentId,TaskId,Mark")] StudentTasks sTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTask);
        }

        //GET: /Student/Delete/5
        public ActionResult DeleteStudentTask(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //POST: /Student/Delete/5
        [HttpPost, ActionName("DeleteStudentTask")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try {
                db.Tasks.Remove(db.Tasks.Find(id));
                var sTasks = new GetStudentTasks().GetStudentTasksByTasksId(id);
                for (int i = 0; i < sTasks.Count; i++)
                {
                    db.StudentTasks.Remove(db.StudentTasks.Find(sTasks[i].Id));
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}
