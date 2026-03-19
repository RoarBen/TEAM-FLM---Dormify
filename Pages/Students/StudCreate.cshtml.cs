using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DormManagementSystem.Data;
using DormManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DormManagementSystem.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (Student.Age < 15 || Student.Age > 100)
            {
                ModelState.AddModelError("Student.Age", "INVALID INPUT: Age must be between 15 and 100.");
            }

            bool exists = await _context.Students.AnyAsync(s => s.StudentID == Student.StudentID);
            if (exists)
            {
                ModelState.AddModelError("Student.StudentID", "StudentID already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Students.Add(Student);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "DATABASE ERROR: The information entered violates system rules (Check Gender or Contact Number format).");
                return Page();
            }
        }
    }
}