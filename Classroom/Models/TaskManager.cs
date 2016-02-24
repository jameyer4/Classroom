using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classroom.Models
{
    public partial class TaskManager
    {
        public int Id { get; set; }
        public int SubjectsId { get; set; }
        public string SubjectName { get; set; }
        public DateTime DateGiven { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public double MarkGiven { get; set; }
    }

    public partial class TaskManager
    {
        public List<TaskManager> TaskList { get; set; } 
    }
}