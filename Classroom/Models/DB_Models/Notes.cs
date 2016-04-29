using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Classroom.Models.DB_Models
{
    public partial class Notes
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MinLength(1)]
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