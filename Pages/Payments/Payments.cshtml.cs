using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DormManagementSystem.Data;
using DormManagementSystem.Models;

namespace DormManagementSystem.Pages.Payments
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) { _context = context; }

        public IList<Payment> PaymentList { get; set; } = default!;
        public List<Registration> Registrations { get; set; } = new();

        [BindProperty]
        public Payment NewPayment { get; set; } = default!;

        public async Task OnGetAsync()
        {
            PaymentList = await _context.Payments.OrderByDescending(p => p.PaymentDate).ToListAsync();

            Registrations = await _context.Registrations.Include(r => r.Student).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            NewPayment.PaymentDate = DateTime.Now;
            NewPayment.AdminID = 1;

            _context.Payments.Add(NewPayment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}