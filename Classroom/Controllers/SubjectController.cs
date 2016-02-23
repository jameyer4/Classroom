using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Classroom.Models;
using Microsoft.Ajax.Utilities;
using NSubstitute;
using NSubstitute.Core;


namespace Classroom.Controllers
{
   // [Authorize(Roles = "User")]
    public class SubjectController : Controller
    {
        private ClassroomContext db = new ClassroomContext();
        List<Subjects> subjectList = new List<Subjects>();

#region Constructors and Index
        public SubjectController()
        {
        }

        public SubjectController(List<Subjects> subjects)
        {
            subjectList = subjects;
        }

        public ActionResult Index()
        {
            return RedirectToAction("ChartMain");
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
            Subjects subjects = new Subjects();
            ///Needs optimisation
            bool studMarks = db.Subject.Any(s => s.Afrikaans>0 && db.Student.Select(x=>x.User.Equals(User.Identity.Name)&&x.Id.Equals(s.StudentId)).Distinct().Count()>1);
            ViewBag.hasMarks = studMarks;
               return View();
            
        }

        public ActionResult Chart()
        {
            var subjects = db.Subject.ToList();
            var students = db.Student.Where(s => s.User.Equals(User.Identity.Name)).ToList();
            ViewBag.errorMessage = "No student subject data to display. ";
            ViewBag.dataFlag = false;

            double eng = 0, afrik, math = 0, nat = 0, geog = 0, hist = 0, life = 0, sum;
            List<double> averages = new List<double>();
            List<string> names = new List<string>();
            int count = 0;
            bool flag = false;
            foreach (var x in subjects)
            {
                try
                {
                    var studId = students[count].Id;
                    if (x.StudentId.Equals(studId))
                    {
                        eng = x.English;
                        afrik = x.Afrikaans;
                        math = x.Math;
                        geog = x.Geography;
                        hist = x.History;
                        life = x.LifeOrientation;
                        nat = x.NaturalScience;
                        sum = afrik + eng + life + math + nat + geog + hist;
                        averages.Add(sum/7);
                        var stud = db.Student.Find(x.StudentId);
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

            if (names.Count < 1||flag)
            {
                ViewBag.dataFlag = true;//There is no data to display
                return View();
            }

            var chart =new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla)
                .AddTitle("Student Averages")
                .AddSeries(name: "Student marks", xValue: names, yValues: averages)
                .Write();

            return View();
        }
#endregion
    }
}
