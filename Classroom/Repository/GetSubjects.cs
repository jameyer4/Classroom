using Classroom.Models.DB_Models;
using System.Collections.Generic;
using System.Linq;
using Classroom.Models;

namespace Classroom.Repository
{
    public class GetSubjects
    {
        private readonly ClassroomContext _db = new ClassroomContext();

        public List<Subject> GetAllSubjects()
        {
            List<Subject> subjects = _db.Subject.ToList();
            return subjects;
        }

        public List<Subject> GetSubjectsById(int id)
        {
            Subject sub = new Subject();
            var list = sub.SubjectList;
            return list;
        }

        //public List<Subject> GetStudentsByTeacher(string user)
        //{
        //    int tId = _db.Teacher.First(t => t.UserName.Equals(user)).Id;
        //    List<Subject> students = _db.Student.Where(x => x.Teacher.Id.Equals(tId)).ToList();
        //    return students;
        //}
    }
}