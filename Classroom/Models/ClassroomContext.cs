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

        

        public System.Data.Entity.DbSet<Student> Student { get; set; }

        public System.Data.Entity.DbSet<Subject> Subject { get; set; }

        public System.Data.Entity.DbSet<Task> Tasks { get; set; }

        public System.Data.Entity.DbSet<Teacher> Teacher { get; set; }

        public System.Data.Entity.DbSet<Class> Class { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().MapToStoredProcedures();
            modelBuilder.Entity<Subject>().MapToStoredProcedures();
            modelBuilder.Entity<Task>().MapToStoredProcedures();
            modelBuilder.Entity<Teacher>().MapToStoredProcedures();
            modelBuilder.Entity<Class>().MapToStoredProcedures();
        }
    }
    
}