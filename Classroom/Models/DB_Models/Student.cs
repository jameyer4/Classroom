﻿namespace Classroom.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
    }
}