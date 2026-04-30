using System;

namespace EduConnect.Exceptions
{
    public class StudentHasActiveEnrollmentsException : Exception
    {
        public StudentHasActiveEnrollmentsException() 
            : base("Cannot delete student because they have active enrollments.") { }

        public StudentHasActiveEnrollmentsException(string message) 
            : base(message) { }
    }
}
