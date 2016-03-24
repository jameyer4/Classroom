using Classroom.Models.DB_Models;
using System.Collections.Generic;
using System.Linq;
using Classroom.Models;
using System;

namespace Classroom.Repository
{
    public class GetSubjects
    {
        private readonly ClassroomContext _db = new ClassroomContext();

        public List<Subject> GetAllSubjects()
        {
            List<Subject> subjects = _db.Subjects.ToList();
            return subjects;
        }

        public Subject GetSubjectById(int id)
        {
            //Subject sub = new Subject();
            try {
                var list = _db.Subjects.Where(s => s.Id.Equals(id)).Single();
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public Subject GetSubjectByMarkId(int id)
        {
            var subjects = GetSubjectById(new GetMarks().GetMarksById(id).First().Id);
            return subjects;
        }
        public List<Subject> GetSubjectsByTeacherId(int id)
        {
            List<Subject> subjects = new List<Subject>();
            var teacher = _db.TeacherSubjects.Where(ts => ts.TeacherId.Equals(id));
            foreach(var item in teacher)
            {
                subjects.Add(GetSubjectById(item.SubjectId));
            }
            return subjects;
        }

        public Subject GetSubjectByName(string name)
        {
            var subject = _db.Subjects.Where(s => s.Name.Equals(name)).Single();
            return subject;
        }

        public List<Subject> GetSubjectsByStudentId(int id)
        {
            var marks = _db.StudentMark.Where(s => s.StudentId.Equals(id));
            List<Subject> subjects = new List<Subject>();
            foreach (var item in marks)
            {
                subjects.Add(GetSubjectById(item.SubjectId));
            }
            return subjects;
        }
        public Subject GetSubjectByTaskId(int id)
        {
            var subjectId = new GetTeacherTasks().GetTasksById(id).SubjectId;
            var subject = GetSubjectById(subjectId);
            return subject;
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