using Classroom.Models;
using Classroom.Models.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classroom.Repository
{
    public class GetTeacherSubjects
    {
        ClassroomContext db = new ClassroomContext();
        public List<TeacherSubjects> GetAllTeacherSubjects()
        {
            var list = db.TeacherSubjects.ToList();
            return list;
        }
        public TeacherSubjects GetTeacherSubjectByTeacherIdAndSubjectId(int tId, int sId)
        {
            try
            {
                var ts = db.TeacherSubjects.Where(t => t.SubjectId.Equals(sId) && t.TeacherId.Equals(tId)).Single();
                return ts;
            }
            catch (Exception ex)
            {
                return null;
            }  
        }
    }
}