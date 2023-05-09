using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using VkCtst.DB;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace VkCtst.Pages.Searching
{
    public interface IPersonService
    {
        Task<List<User>> GetPaginatedResult(int currentPage, int pageSize = 4);
        Task<int> GetCount();
    }
    public class PersonService : IPersonService 
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private ApplicationDbContext _db;
        public PersonService(IHostingEnvironment hostingEnvironment, ApplicationDbContext db)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
        }
        public async Task<List<User>> GetPaginatedResult(int currentPage, int pageSize = 4)
        {
            var data = await GetData();
            return data.OrderBy(d => d.login).Skip((currentPage- 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<int> GetCount()
        {
            var data = await GetData();
            return data.Count;
        }
        private async Task<List<User>> GetData()
        {
            List<User> users = await _db.Users.AsNoTracking().ToListAsync();
            return users;
        }
    }
}
