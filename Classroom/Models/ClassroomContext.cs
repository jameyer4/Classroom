using Classroom.Models.DB_Models;
using System.Data.Entity;

namespace Classroom.Models
{
    public class ClassroomContext:DbContext
    {
        //public ClassroomContext() : base("name=ClassroomContext")
        //{
        //    Database.SetInitializer<ClassroomContext>(null);
        //}

        public DbSet<Student> Student { get; set; }

        public DbSet<Subject> Subject { get; set; }

        public DbSet<TaskManager> Tasks { get; set; }

        public DbSet<Teacher> Teacher { get; set; }

        public DbSet<Class> Class { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Student>().MapToStoredProcedures();
        //    modelBuilder.Entity<Subject>().MapToStoredProcedures();
        //    modelBuilder.Entity<TaskManager>().MapToStoredProcedures();
        //    modelBuilder.Entity<Teacher>().MapToStoredProcedures();
        //    modelBuilder.Entity<Class>().MapToStoredProcedures();
        //}
    }
    
}