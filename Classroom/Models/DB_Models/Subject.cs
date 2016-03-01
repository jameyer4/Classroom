using System.Collections.Generic;

namespace Classroom.Models.DB_Models
{
    public partial class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int TeachertId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public double Mark { get; set; }
        public virtual Class Class { get; set; }
        public int ClassId { get; set; }
    }
    public partial class Subject
    {
        public List<Subject> SubjectList { get; set; }
    }
}