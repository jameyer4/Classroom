using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Classroom.Models;
using Microsoft.Ajax.Utilities;
using Classroom.Models.DB_Models;

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
            //return RedirectToAction("ChartMain"); --Uncomment to see average chart
            return View();
        }

        #endregion

        #region ChartMain
        public ActionResult ChartMain()
        {
            ViewBag.DataFlag = false;
            if (User.Identity.Name.IsNullOrWhiteSpace())
            {
                ViewBag.DataFlag = true;
            }
            Subject subjects = new Subject();
            ///Needs optimisation
            ///Check if the student has 

            bool studMarks = db.Subject.Any(s => s.Mark > 0 && db.Teacher.Select(x => x.UserName.Equals(User.Identity.Name) && x.Id.Equals(s.Student.Id)).Distinct().Count() > 1);
            ViewBag.hasMarks = studMarks;
            return View();
        }

        public ActionResult Chart()
        {
            var subjects = db.Subject.ToList();
            var students = db.Student.Where(s => s.Teacher.UserName.Equals(User.Identity.Name)).ToList();
            ViewBag.errorMessage = "No student subject data to display. ";
            ViewBag.dataFlag = false;

            List<double> averages = new List<double>();
            List<string> names = new List<string>();
            int count = 0;
            bool flag = false;
            foreach (var x in subjects)
            {
                try
                {
                    var studId = students[count].Id;
                    if (x.Student.Id.Equals(studId))
                    {
                        var classSubj = db.Subject.Where(s => s.Class.Id.Equals(studId)).ToList();
                        double mark = 0, sum = 0;
                        for (int i = 0; i < classSubj.Count; i++)
                        {
                            mark = classSubj[i].Mark;
                            sum += mark;
                        }
                        averages.Add(sum / 7);
                        var stud = db.Student.Find(x.Student.Id);
                        names.Add(stud.FirstName + " " + stud.LastName);
                    }
                    count++;
                }
                catch (Exception ex)
                {
                    //log issue or get better handling for no student ID error
                    flag = true;
                    break;
                }
            }

            ViewBag.Averages = averages;
            ViewBag.Names = names;

            if (names.Count < 1 || flag)
            {
                ViewBag.dataFlag = true;//There is no data to display
                return View();
            }

            var chart = new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla)
                .AddTitle("Student Averages")
                .AddSeries(name: "Student marks", xValue: names, yValues: averages)
                .Write();

            return View();
        }

        #endregion
        [HttpGet]
        [System.Web.Http.Route("Subject/SubjectView/{subject}")]
        public ActionResult SubjectView(string subject)
        {
            ViewBag.Subject = subject;
            return View(TaskManager(subject));
        }

        private List<TaskManager> TaskManager(string subject)
        {
            List<TaskManager> model = new List<TaskManager>();
            var tasks = db.Tasks.ToList();
            //var c1 = from ;
            // var check = db.Subject.Where(x => x.Id.Equals(c1));

            //switch (subject)
            //{
            //    case "Math": var subjectGroup = model.Where(s => s.SubjectName.Equals(subject)&&s.SubjectsId.Equals())).ToList();        
            //}
            return (model);
        }
    }
}
