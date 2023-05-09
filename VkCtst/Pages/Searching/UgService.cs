using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using VkCtst.DB;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace VkCtst.Pages.Searching
{
    public interface IUgService
    {
        Task<List<User_Group>> GetPaginatedResult(int currentPage, int pageSize = 4);
        Task<int> GetCount();
    }
    public class UgService : IUgService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private ApplicationDbContext _db;
        public UgService(IHostingEnvironment hostingEnvironment, ApplicationDbContext db)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
        }
        public async Task<List<User_Group>> GetPaginatedResult(int currentPage, int pageSize = 4)
        {
            var data = await GetData();
            return data.OrderBy(d => d.id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<int> GetCount()
        {
            var data = await GetData();
            return data.Count;
        }
        private async Task<List<User_Group>> GetData()
        {
            List<User_Group> users = await _db.User_Groups.AsNoTracking().ToListAsync();
            return users;
        }
    }
}
