using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Classroom.Models;
using Microsoft.Ajax.Utilities;
using NSubstitute;
using NSubstitute.Extensions;

namespace Classroom.Repository
{
    public class GetStudents
    {
        private readonly ClassroomContext _db = new ClassroomContext();

        public List<Student> GetAllStudents()
        {
            List<Student> students = _db.Student.ToList();
            return students;
        }

        public Student GetStudentById(int id)
        {
            Student student = _db.Student.Single(x => x.Id == id);
            return student;
        }

        public List<Student> GetStudentsByUser(string user)
        {
            List<Student> students = _db.Student.Where(x => x.User.Equals(user)).ToList();
            return students;
        }
    }
}