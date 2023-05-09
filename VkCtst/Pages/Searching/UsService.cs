using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using VkCtst.DB;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace VkCtst.Pages.Searching
{
    public interface IUsService
    {
        Task<List<User_State>> GetPaginatedResult(int pageNumber, int pageSize = 4);
        Task<int> GetCount();
    }
    public class UsService : IUsService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private ApplicationDbContext _db;
        public UsService(IHostingEnvironment hostingEnvironment, ApplicationDbContext db)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
        }
        public async Task<List<User_State>> GetPaginatedResult(int currentPage, int pageSize = 4)
        {
            var data = await GetData();
            return data.OrderBy(d => d.id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<int> GetCount()
        {
            var data = await GetData();
            return data.Count;
        }
        private async Task<List<User_State>> GetData()
        {
            List<User_State> users = await _db.User_States.AsNoTracking().ToListAsync();
            return users;
        }
    }
}
