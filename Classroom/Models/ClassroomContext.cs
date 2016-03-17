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

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<Teacher> Teacher { get; set; }

        public DbSet<StudentMark> StudentMark { get; set; }

        public DbSet<TeacherSubjects> TeacherSubjects { get; set; }

        public DbSet<StudentTasks> StudentTasks { get; set; }

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