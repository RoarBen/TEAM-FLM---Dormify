using System.ComponentModel.DataAnnotations;
namespace DormManagementSystem.Models
{
    public class Student
    {
        [Key]
        public string StudentID { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;
    }
}