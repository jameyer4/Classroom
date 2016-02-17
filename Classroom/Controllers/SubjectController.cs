﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Classroom.Models;
using NUnit.Framework;

namespace Classroom.Controllers
{
    public class SubjectController : Controller
    {
        private ClassroomContext db = new ClassroomContext();

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult ChartMain()
        {
            var subjects = db.Subject.ToList();
            var students = db.Student.ToList();

            double eng = 0, afrik, math = 0, nat = 0, geog = 0, hist = 0, life = 0, sum;
            List<double> averages = new List<double>();
            List<string> names = new List<string>();

            foreach (var x in subjects)
            {
                eng = x.English;
                afrik = x.Afrikaans;
                math = x.Math;
                geog = x.Geography;
                hist = x.History;
                life = x.LifeOrientation;
                nat = x.NaturalScience;
                sum = afrik + eng + life + math + nat + geog + hist;
                averages.Add(sum / 7);
                var stud = db.Student.Find(x.StudentId);
                names.Add(stud.FirstName + " " + stud.LastName);
            }

            ViewBag.Averages = averages;
            ViewBag.Names = names;
            return View();
        }
    }
}