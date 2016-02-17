using System;
using System.Collections.Generic;
using System.Linq;
using Classroom.Controllers;
using Classroom.Models;
using NUnit.Framework;

namespace Classroom.Tests
{
    [TestFixture]
    public class TestStudent
    {
        [Test]
        public void TestStudentCreation()
        {
            var student = GetTestStudent();
            var controller = new StudentController(student);

            var result = controller.Create();
            Console.WriteLine("Result: "+result+", Expected: "+student);
            Assert.AreEqual(student, result);
        }
        [Test]
        public void TestListStudentCreation()
        {
            var student = GetTestStudentList();
            var controller = new StudentController(student);
            var list = student.Select(x => controller.Create()).ToList();

            //Console.WriteLine("Result: " + result + ", Expected: " + student);
            Assert.AreEqual(student.Count, list.Count);
        }

        public Student GetTestStudent()
        {
          //  var testStudents = new List<Student>();
            var student =new Student{Age=13,FirstName = "Ben",Id = 11,LastName = "Hur"};

            return student;
        }

        public List<Student> GetTestStudentList()
        {
            var testStudents = new List<Student>();
            testStudents.Add(new Student{Age = 11,FirstName = "Ben",Id = 11,LastName = "Hur"});
            testStudents.Add(new Student{Age = 12,FirstName = "fef",Id = 12,LastName = "dsa"});
            testStudents.Add(new Student {Age = 32, FirstName = "dwdwd", Id = 13, LastName = "qdq"});

            return testStudents;
        }

    }
}