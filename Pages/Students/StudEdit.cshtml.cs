using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DormManagementSystem.Data;
using DormManagementSystem.Models;

namespace DormManagementSystem.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context) { _context = context; }

        [BindProperty]
        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students.FirstOrDefaultAsync(m => m.StudentID == id);

            if (student == null) return NotFound();

            Student = student;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Student.Age < 15 || Student.Age > 100)
            {
                ModelState.AddModelError("Student.Age", "INVALID INPUT: Age must be between 15 and 100.");
            }

            if (!ModelState.IsValid) return Page();

            _context.Attach(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.StudentID)) return NotFound();
                else throw;
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "DATABASE ERROR: One of the inputs violates a SQL rule (Check Gender or Contact Number).");
                return Page();
            }
        }

        private bool StudentExists(string id) => _context.Students.Any(e => e.StudentID == id);
    }
}