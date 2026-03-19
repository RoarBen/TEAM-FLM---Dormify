using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DormManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace DormManagementSystem.Pages.Maintenance
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context) { _context = context; }

        [BindProperty]
        public Models.Maintenance Maintenance { get; set; } = default!;
        public SelectList RoomList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var rooms = await _context.Rooms.OrderBy(r => r.RoomID).ToListAsync();
            RoomList = new SelectList(rooms, "RoomID", "RoomNumber");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Maintenance.MaintenanceID");
            if (!ModelState.IsValid) return Page();

            _context.Maintenance.Add(Maintenance);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}