using System.Collections.Generic;
using System.Linq;
using Classroom.Models;

namespace Classroom.Repository
{
    public class GetTeachers
    {
        private readonly ClassroomContext _db = new ClassroomContext();

        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> students = _db.Teacher.ToList();
            return students;
        }

        public Teacher GetTeacherById(int id)
        {
            Teacher student = _db.Teacher.Single(x => x.Id == id);
            return student;
        }
    }
}