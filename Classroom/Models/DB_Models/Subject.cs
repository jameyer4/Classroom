using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Classroom.Models
{
    public class Subject : IEnumerable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Mark { get; set; }
        //[Key,ForeignKey("FK_Teacher")]
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
        //[Key, ForeignKey("FK_Class")]
        public Class Class { get; set; }
        public int ClassId { get; set; }    
        //[Key, ForeignKey("FK_Student")]
        public Student Student { get; set; }
        public int StudentId { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}