using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classroom.Models.DB_Models
{
    public class TeacherSubjects
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
    }
}