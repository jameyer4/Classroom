using System.ComponentModel.DataAnnotations.Schema;

namespace Classroom.Models.DB_Models
{
    public class StudentMark
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        [NotMapped]
        public Subject Subject { get; set; }
        public double Mark { get; set; }
    }
}