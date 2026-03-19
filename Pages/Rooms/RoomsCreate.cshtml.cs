using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DormManagementSystem.Data;
using DormManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DormManagementSystem.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context) { _context = context; }

        [BindProperty]
        public Room Room { get; set; } = default!;

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Room.RoomID");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "DEBUG: May mali sa input fields mo. Check properly.");
                return Page();
            }

            try
            {
                _context.Rooms.Add(Room);
                await _context.SaveChangesAsync();

                // Siguraduhin na "Index" ang pangalan ng listahan mo
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Huhulihin nito kung bakit ayaw tanggapin ng DormitoryDB
                ModelState.AddModelError(string.Empty, "DATABASE ERROR: " + (ex.InnerException?.Message ?? ex.Message));
                return Page();
            }
        }
    }
}