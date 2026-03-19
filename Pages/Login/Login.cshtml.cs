using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DormManagementSystem.Data;

namespace DormManagementSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public LoginModel(ApplicationDbContext context) { _context = context; }

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == Username && a.PasswordHash == Password);

            if (admin != null)
            {
                return RedirectToPage("/Dashboard/Index");
            }

            ErrorMessage = "Wrong Username or Password";
            return Page();
        }
    }
}