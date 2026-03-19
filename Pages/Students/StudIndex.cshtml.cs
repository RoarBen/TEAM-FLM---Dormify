using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DormManagementSystem.Data;
using DormManagementSystem.Models;

namespace DormManagementSystem.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) { _context = context; }

        public IList<Student> StudentList { get; set; } = default!;

        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            StudentList = await _context.Students.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToPage();

            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                bool hasReg = await _context.Registrations.AnyAsync(r => r.StudentID == id);
                if (hasReg)
                {
                    ErrorMessage = "CANNOT DELETE: Student has an active registration.";
                    return RedirectToPage();
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}