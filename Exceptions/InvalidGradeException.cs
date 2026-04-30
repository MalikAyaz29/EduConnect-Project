using System;

namespace EduConnect.Exceptions
{
    public class InvalidGradeException : Exception
    {
        public InvalidGradeException() 
            : base("The grade entered is invalid. It must be between 0 and 100.") { }

        public InvalidGradeException(string message) 
            : base(message) { }
    }
}
