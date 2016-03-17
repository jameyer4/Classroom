using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Classroom.Models.DB_Models
{
    public partial class Tasks
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public DateTime DateGiven { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }
    }

    //public partial class TaskManager
    //{
    //    [NotMapped]
    //    public List<TaskManager> TaskList { get; set; } 
    //}
}