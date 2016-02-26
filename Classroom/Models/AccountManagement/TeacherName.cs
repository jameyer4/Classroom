using Microsoft.AspNet.Identity.EntityFramework;

namespace Classroom.Models.AccountManagement
{
    public class TeacherName : IdentityUser
    {
        public string Name { get; set; }
    }
}