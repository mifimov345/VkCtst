using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VkCtst.DB;

namespace VkCtst.Pages.Adding
{
    [IgnoreAntiforgeryToken]
    public class AddIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public SelectList UserG_state { get; set; }

        public AddIndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void UserG_Choice(ApplicationDbContext _context)
        {
            var UserQuery = _context.User_Groups.Select(x => x.code).ToList();
            if (UserQuery.Contains("Admin"))
            {
                string[] choices = { "User" };
                UserG_state= new SelectList(choices);
            }
            else
            {
                string[] choices = { "Admin", "User" };
                UserG_state = new SelectList(choices);
            }
        }

        [BindProperty]
        public User User_ { get; set; } = new();
        [BindProperty]
        public User_Group User_Group_ { get; set; } = new();
        [BindProperty]
        public User_State User_State_ { get; set; } = new();
        

        public async Task<IActionResult> OnPostAsync()
        {
            var TempUsers = _db.Users.Select(x => x.login).ToList();
            if (ModelState.IsValid)
            {
                await Task.Delay(5000);
                if (TempUsers.Contains(User_.login))
                {
                    return RedirectToPage("ErrorAdding");
                }
                User_State_.code = "Active";
                await _db.User_Groups.AddAsync(User_Group_);
                await _db.User_States.AddAsync(User_State_);
                User_.created_date = DateTime.Now;
                User_.user_group_id = User_Group_;
                User_.user_state_id= User_State_;
                await _db.Users.AddAsync(User_);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            UserG_Choice(_db);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetAsync()
        {
            UserG_Choice(_db);
            return Page();
        }
    }
}
