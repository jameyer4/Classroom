using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Classroom.Models;


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
            var subjects = db.Subject.ToList();
            var students = db.Student.Where(s=>s.User.Equals(User.Identity.Name)).ToList();
            ViewBag.errorMessage = "No student subject data to display. ";
            ViewBag.dataFlag = false;

            double eng = 0, afrik, math = 0, nat = 0, geog = 0, hist = 0, life = 0, sum;
            List<double> averages = new List<double>();
            List<string> names = new List<string>();
            int count = 0;
            foreach (var x in subjects)
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

            ViewBag.Averages = averages;
            ViewBag.Names = names;

            if (names.Count < 1)
            {
                ViewBag.dataFlag = true;//There is no data to display
            }
            return View();
        }
#endregion
    }
}
