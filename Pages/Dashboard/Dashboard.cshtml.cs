using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DormManagementSystem.Data;
using DormManagementSystem.Models;

namespace DormManagementSystem.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) { _context = context; }

        public decimal TotalRevenue { get; set; }
        public int StudentCount { get; set; }
        public int AvailableRooms { get; set; }
        public int PendingMaintenance { get; set; }
        public List<DormManagementSystem.Models.Maintenance> RecentMaintenance { get; set; } = new();

        public async Task OnGetAsync()
        {
            StudentCount = await _context.Students.CountAsync();
            var payments = await _context.Payments.ToListAsync();
            TotalRevenue = payments.Sum(p => p.Amount);
            AvailableRooms = await _context.Rooms.CountAsync(r => r.RoomStatus == "Available");
            PendingMaintenance = await _context.Maintenance.CountAsync(m => m.MaintenanceStatus == "Pending");

            RecentMaintenance = await _context.Maintenance
                .OrderByDescending(m => m.MaintenanceID)
                .Take(5)
                .ToListAsync();
        }
    }
}