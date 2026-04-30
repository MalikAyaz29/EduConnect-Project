using System;
using System.Collections.Generic;
using EduConnect.Models;

namespace EduConnect.Interfaces
{
    public interface IGradeService
    {
        IEnumerable<GradeRecord> GetGradesByStudent(Guid studentId);
        IEnumerable<GradeRecord> GetGradesByCourse(Guid courseId);
        
        void SubmitGrade(Guid studentId, Guid courseId, double marks);
        
        double CalculateCGPA(Guid studentId);

        event Action OnGradesSubmitted;
    }
}
