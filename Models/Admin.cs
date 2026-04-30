using EduConnect.Enums;

namespace EduConnect.Models
{
    // SOLID Principle: Liskov Substitution Principle (LSP)
    public class Admin : Person
    {
        public override Role Role => Role.Admin;
        
        public string OfficeLocation { get; set; } = string.Empty;
    }
}
