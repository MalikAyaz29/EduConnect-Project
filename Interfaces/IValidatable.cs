using System.Collections.Generic;

namespace EduConnect.Interfaces
{
    // Used for custom model validations (e.g. validating a Student when adding)
    public interface IValidatable
    {
        bool IsValid();
        IEnumerable<string> GetValidationErrors();
    }
}
