using System.Collections.Generic;
using System.Linq;
using Classroom.Controllers;
using Classroom.Models;
using NUnit.Framework;

namespace Classroom.Tests
{
    [TestFixture]
    public class TestSubjects
    {
        [Test]
        public void TestChartMain()
        {
            var subjects = GetTestSubjectsList();
            var controller = new SubjectController(subjects);
            var list = subjects.Select(x => controller.ChartMain()).ToList();

            Assert.AreEqual(subjects.Count, list.Count);

        }

        public List<Subjects> GetTestSubjectsList()
        {
            var testSubjects = new List<Subjects>();
            testSubjects.Add(new Subjects { Id = 1, StudentId = 1, Afrikaans = 70, English = 78, Math = 88, NaturalScience = 87, Geography = 77, History = 60, LifeOrientation = 90 });
            testSubjects.Add(new Subjects { Id = 3, StudentId = 6, Afrikaans = 76, English = 65, History = 59, Geography = 62, LifeOrientation = 71, Math = 61, NaturalScience = 62 });
            testSubjects.Add(new Subjects { Id = 2, StudentId = 7, Math = 55, Afrikaans = 48, English = 52, Geography = 63, History = 62, LifeOrientation = 51, NaturalScience = 52 });

            return testSubjects;
        }
    }
}