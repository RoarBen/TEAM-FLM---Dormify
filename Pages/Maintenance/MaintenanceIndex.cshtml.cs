using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DormManagementSystem.Data;
using DormManagementSystem.Models;

namespace DormManagementSystem.Pages.Maintenance
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) { _context = context; }

        public IList<Models.Maintenance> MaintenanceList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            MaintenanceList = await _context.Maintenance.OrderByDescending(m => m.MaintenanceID).ToListAsync();
        }
        public async Task<IActionResult> OnPostUpdateStatusAsync(int id)
        {
            var report = await _context.Maintenance.FindAsync(id);
            if (report != null)
            {
                report.MaintenanceStatus = (report.MaintenanceStatus == "Pending") ? "Resolved" : "Pending";
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var report = await _context.Maintenance.FindAsync(id);
            if (report != null)
            {
                _context.Maintenance.Remove(report);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}