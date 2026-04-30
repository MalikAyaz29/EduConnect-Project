using System;

namespace EduConnect.Exceptions
{
    public class CourseFullException : Exception
    {
        public CourseFullException() 
            : base("Cannot enroll because the course is full.") { }

        public CourseFullException(string message) 
            : base(message) { }
    }
}
