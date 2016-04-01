using Classroom.Models;
using Classroom.Models.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Classroom.Repository
{   
    public class GetStudentTasks
    {
        ClassroomContext db = new ClassroomContext();
        public StudentTasks GetStudentTasksById(int id)
        {
            var stask = db.StudentTasks.Where(s => s.Id.Equals(id)).Single();
            return stask;
        }
        public List<StudentTasks> GetStudentTasksByTasksId(int id)
        {
            var tasks = db.StudentTasks.Where(s => s.TaskId.Equals(id)).ToList();
            return tasks;
        }
        public double GetStudentAverageByStudentId(int id)
        {
            var sTasks = db.StudentTasks.Where(s => s.StudentId.Equals(id)).ToList();
            double avrg = 0;
            foreach(var item in sTasks)
            {
                avrg += item.Mark;
            }
            return avrg / sTasks.Count;
        }
        public List<StudentTasks> GetStudentTasksByStudentId(int id)
        {
            var tasks = db.StudentTasks.Where(s => s.StudentId.Equals(id)).ToList();
            return tasks;
        }
    }
}