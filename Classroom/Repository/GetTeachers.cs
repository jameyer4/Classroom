using System.Collections.Generic;
using System.Linq;
using Classroom.Models;
using Classroom.Models.DB_Models;

namespace Classroom.Repository
{
    public class GetTeachers
    {
        private readonly ClassroomContext _db = new ClassroomContext();
        Repository rep = new Repository();
        //GetMarks marks = new GetMarks();
        //GetSubjects subjects = new GetSubjects();
        public GetTeachers()
        {

        }

        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = _db.Teacher.ToList();
            return teachers;
        }

        public Teacher GetTeacherById(int id)
        {
            Teacher teacher = _db.Teacher.Single(x => x.Id == id);
            return teacher;
        }

        public Teacher GetTeacherByUsername(string name)
        {
            Teacher teacher = new ClassroomContext().Teacher.Where(x => x.UserName.Equals(name)).Single();
            return teacher;
        }
        public string GetTeacherUsernameById(int id)
        {
            var username = _db.Teacher.Single(x => x.Id == id).UserName;
            return username;
        }
        public int GetTeacherIdByUsername(string name)
        {
            var id = GetTeacherByUsername(name).Id;
            return id;
        }
        public List<Teacher> GetTeachersByStudentId(int id)
        {
            
            var markList = new GetMarks().GetMarksByStudentId(id);
            //var subjectList = subjects.GetSubjectByMarkId(.id;
            List<Subject> subjectList = new List<Subject>();
            foreach(var x in markList)
            {
                subjectList.Add(new GetSubjects().GetSubjectByMarkId(x.Id));
            }
            List<Teacher> teachers = new List<Teacher>();
            foreach (var x in subjectList)
            {
                teachers.Add(GetTeacherById(x.Id));
            }
            return teachers;
        }
    }
}