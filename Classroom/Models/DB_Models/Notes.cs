using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classroom.Models.DB_Models
{
    public partial class Notes
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateSet { get; set; }
        public DateTime DueDate { get; set; }
    }
    public partial class Notes
    {
        [NotMapped]
        public List<Notes> NotesList { get; set; }
    }
}