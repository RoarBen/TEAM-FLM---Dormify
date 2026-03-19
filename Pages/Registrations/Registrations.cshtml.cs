using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DormManagementSystem.Data;
using DormManagementSystem.Models;

namespace DormManagementSystem.Pages.Registrations
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) { _context = context; }

        [BindProperty]
        public Registration NewRegistration { get; set; } = default!;
        public SelectList StudentOptions { get; set; } = default!;
        public SelectList RoomOptions { get; set; } = default!;

        public class RegistrationViewModel
        {
            public int RegistrationID { get; set; }
            public string StudentName { get; set; } = string.Empty;
            public string RoomNumber { get; set; } = string.Empty;
            public DateTime RegistrationDate { get; set; }
            public string Status { get; set; } = string.Empty;
        }

        public IList<RegistrationViewModel> RegistrationList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // 1. I-load ang Dropdown Options
            var students = await _context.Students.OrderBy(s => s.FullName).ToListAsync();
            StudentOptions = new SelectList(students, "StudentID", "FullName");

            var rooms = await _context.Rooms.Where(r => r.RoomStatus == "Available").ToListAsync();
            RoomOptions = new SelectList(rooms, "RoomID", "RoomNumber");

            // 2. I-load ang Table List
            RegistrationList = await (from reg in _context.Registrations
                                      join stu in _context.Students on reg.StudentID equals stu.StudentID
                                      join rm in _context.Rooms on reg.RoomID equals rm.RoomID
                                      select new RegistrationViewModel
                                      {
                                          RegistrationID = reg.RegID,
                                          StudentName = stu.FullName,
                                          RoomNumber = rm.RoomNumber,
                                          RegistrationDate = reg.CheckInDate,
                                          Status = reg.Status
                                      }).ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            NewRegistration.CheckInDate = DateTime.Now;
            _context.Registrations.Add(NewRegistration);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var relatedPayments = _context.Payments.Where(p => p.RegID == id);
            _context.Payments.RemoveRange(relatedPayments);

            var reg = await _context.Registrations.FindAsync(id);
            if (reg != null)
            {
                _context.Registrations.Remove(reg);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}