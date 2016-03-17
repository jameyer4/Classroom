using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classroom.Models.DB_Models
{
    public class StudentTasks
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TaskId { get; set; }
        public double Mark { get; set; }
    }
}