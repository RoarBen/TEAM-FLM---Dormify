using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormManagementSystem.Models
{
    public class Registration
    {
        [Key]
        public int RegID { get; set; }

        public string StudentID { get; set; } = string.Empty;

        [ForeignKey("StudentID")]
        public virtual Student? Student { get; set; }

        public int RoomID { get; set; }

        // ⬇️ DAGDAG MO ITONG DALAWA PARA MAWALA ANG RED LINES SA SCREENSHOT MO
        public string Status { get; set; } = "Active";
        public int AdminID { get; set; }

        public DateTime CheckInDate { get; set; } = DateTime.Now;
    }
}