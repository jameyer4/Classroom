namespace Classroom.Models
{
    public class Subjects
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public double English { get; set; }
        public double Afrikaans { get; set; }
        public double Math { get; set; }
        public double NaturalScience { get; set; }
        public double Geography { get; set; }
        public double History { get; set; }
        public double LifeOrientation { get; set; }
    }
}