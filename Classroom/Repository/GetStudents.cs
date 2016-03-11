using System.Collections.Generic;
using System.Linq;
using Classroom.Models.DB_Models;
using Classroom.Models;

namespace Classroom.Repository
{
    public class GetStudents
    {
      //  GetTeachers teachers = new GetTeachers();
       // GetMarks marks = new GetMarks();
       // GetSubjects subjects = new GetSubjects();

        private readonly ClassroomContext _db = new ClassroomContext();

        public List<Student> GetAllStudents()
        {
            List<Student> students = _db.Students.ToList();
            return students;
        }

        public Student GetStudentById(int id)
        {
            Student student = _db.Students.Where(x => x.Id.Equals(id)).Single();
            return student;
        }

        public List<Student> GetStudentsByTeacherUsername(string user)
        {
            var teacherId = new GetTeachers().GetTeacherIdByUsername(user);
            var markList = new GetMarks().GetMarksByTeacherId(teacherId).Where(m => m.SubjectId.Equals(1));
            List<Student> students = new List<Student>();
            foreach (var x in markList)
            {
                students.Add(GetStudentById(x.StudentId));
            }
            return students;
        }
    }
}