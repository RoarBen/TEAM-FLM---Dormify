using System.ComponentModel.DataAnnotations;

namespace DormManagementSystem.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public int RegID { get; set; }
        public int AdminID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
    }
}