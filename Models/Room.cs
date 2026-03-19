using System.ComponentModel.DataAnnotations;

namespace DormManagementSystem.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string RoomStatus { get; set; } = "Available";
        public virtual ICollection<Registration>? Registrations { get; set; }
    }
}