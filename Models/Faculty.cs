using EduConnect.Enums;

namespace EduConnect.Models
{
    // SOLID Principle: Liskov Substitution Principle (LSP)
    public class Faculty : Person
    {
        public override Role Role => Role.Faculty;

        public string Department { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
