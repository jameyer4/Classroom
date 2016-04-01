using Classroom.Models;
using Classroom.Models.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classroom.Repository
{
    public class GetTeacherTasks
    {
        ClassroomContext db = new ClassroomContext();
        public List<Tasks> GetAllTeacherTasks(int id)
        {
            var list = db.Tasks.Where(t=>t.TeacherId.Equals(id)).ToList();
            return list;
        }
        public Tasks GetTaskByTeacherIdAndSubjectId(int tId, int sId)
        {
            try
            {
                var ts = db.Tasks.Where(t => t.SubjectId.Equals(sId) && t.TeacherId.Equals(tId)).Single();
                return ts;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Tasks GetTasksById(int id)
        {
            var task = db.Tasks.Where(t => t.Id.Equals(id)).Single();
            return task;
        }
    }
}