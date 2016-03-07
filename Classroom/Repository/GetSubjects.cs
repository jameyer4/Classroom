using Classroom.Models.DB_Models;
using System.Collections.Generic;
using System.Linq;
using Classroom.Models;

namespace Classroom.Repository
{
    public class GetSubjects
    {
        private readonly ClassroomContext _db = new ClassroomContext();
        //GetMarks marks = new GetMarks();
        //GetTeachers teachers = new GetTeachers();
        public List<Subject> GetAllSubjects()
        {
            List<Subject> subjects = _db.Subjects.ToList();
            return subjects;
        }

        public List<Subject> GetSubjectsById(int id)
        {
            //Subject sub = new Subject();
            var list = _db.Subjects.Where(s => s.Id.Equals(id)).ToList();
            return list;
        }
        public Subject GetSubjectByMarkId(int id)
        {
            var subjects = GetSubjectsById(new GetMarks().GetMarksById(id).First().Id).First();
            return subjects;
        }
        public List<Subject> GetSubjectByTeacherId(int id)
        {
            var subjects = GetSubjectsById(new GetTeachers().GetTeacherById(id).Id).ToList();
            return subjects;
        }

       // public List<Subject> GetSubjectByStudentId()

        //public List<Subject> GetStudentsByTeacher(string user)
        //{
        //    int tId = _db.Teacher.First(t => t.UserName.Equals(user)).Id;
        //    List<Subject> students = _db.Student.Where(x => x.Teacher.Id.Equals(tId)).ToList();
        //    return students;
        //}
    }
}