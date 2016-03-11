using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classroom.Models.DB_Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        
        public int? TaskId { get; set; }
                                           //  public string Password { get; set; }
    }
}