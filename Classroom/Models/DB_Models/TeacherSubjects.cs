namespace Classroom.Models.DB_Models
{
    public class TeacherSubjects
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
    }
}