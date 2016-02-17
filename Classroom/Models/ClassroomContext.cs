using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Classroom.Models
{
    public class ClassroomContext:DbContext
    {
        public ClassroomContext() : base("name=ClassroomContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public System.Data.Entity.DbSet<Classroom.Models.Student> Student { get; set; }

        public System.Data.Entity.DbSet<Classroom.Models.Subjects> Subject { get; set; }
    }
    
}