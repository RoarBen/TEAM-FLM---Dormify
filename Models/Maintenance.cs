using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormManagementSystem.Models
{
    public class Maintenance
    {
        [Key]
        [Column("ReportID")] 
        public int MaintenanceID { get; set; }

        public int RoomID { get; set; }

        [Column("IssueDescription")] 
        public string? Description { get; set; }

        public string MaintenanceStatus { get; set; } = "Pending";

        
        
    }
}