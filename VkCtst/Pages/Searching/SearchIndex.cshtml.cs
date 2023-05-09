using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VkCtst.DB;

namespace VkCtst.Pages.Searching
{
    public class SearchIndexModel : PageModel
    {
        private readonly IPersonService _personService;
        private readonly IUgService _UgService;
        private readonly IUsService _UsService;
        private ApplicationDbContext _db;
        public SearchIndexModel(IPersonService personService,IUsService usService, IUgService ugService, ApplicationDbContext db)
        {
            _UsService= usService;
            _UgService = ugService;
            _personService = personService;
            _db = db;
        }
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 4;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public List<User> Users { get; set; }
        public List<User_Group> Groups { get; set; }
        public List<User_State> States { get; set; }
        public async Task OnGetAsync()
        {
            Users = await _personService.GetPaginatedResult(CurrentPage, PageSize);
            Groups = await _UgService.GetPaginatedResult(CurrentPage, PageSize);
            States = await _UsService.GetPaginatedResult(CurrentPage, PageSize);
            Count = await _personService.GetCount();
        }
    }
}
