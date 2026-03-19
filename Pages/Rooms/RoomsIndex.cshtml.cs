using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DormManagementSystem.Data;
using DormManagementSystem.Models;

namespace DormManagementSystem.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) { _context = context; }

        public IList<Room> RoomList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            RoomList = await _context.Rooms
                .Include(r => r.Registrations)
                    .ThenInclude(reg => reg.Student)
                .ToListAsync();

            if (!RoomList.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    _context.Rooms.Add(new Room
                    {
                        RoomNumber = "Room " + i,
                        Capacity = 4,
                        RoomStatus = "Available"
                    });
                }
                await _context.SaveChangesAsync();
                RoomList = await _context.Rooms.Include(r => r.Registrations).ThenInclude(reg => reg.Student).ToListAsync();
            }
        }
    }
}