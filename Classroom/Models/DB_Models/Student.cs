using System.Collections.Generic;

namespace Classroom.Models.DB_Models
{
    public class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Student()
        //{
        //    this.StudentMarks = new HashSet<StudentMarks>();
        //}
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
 //       public virtual List<StudentMark> StudentMarks { get; set; }
    }
}