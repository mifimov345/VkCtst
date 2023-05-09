using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VkCtst.DB;

namespace VkCtst.Pages.Deleting
{
    [IgnoreAntiforgeryToken]
    public class DeletingIndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public DeletingIndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<User> Users_ { get; set; } = new();
        public List<string> State_ { get; set; } = new();
        public User Users { get; set; } = new();
        public List<User_Group> Groups { get; set; }
        public List<User_State> States { get; set; }

        public async Task OnGet()
        {
            Users_ = await _db.Users.AsNoTracking().ToListAsync();
            Groups = await _db.User_Groups.AsNoTracking().ToListAsync();
            States = await _db.User_States.AsNoTracking().ToListAsync();
            State_ = _db.User_States.AsNoTracking().Select(x => x.code).ToList();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            var userd = await _db.Users
                .Include(x => x.user_state_id)
                .FirstOrDefaultAsync(x => x.id == id);
            userd.user_state_id.code = "Blocked";
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
