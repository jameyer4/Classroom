using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Classroom.Models.DB_Models
{
    public partial class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
    public partial class Subject
    {
        [NotMapped]
        public List<Subject> SubjectList { get; set; }
    }
}