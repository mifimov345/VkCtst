using Microsoft.EntityFrameworkCore;
using VkCtst.DB;
using VkCtst.Pages.Searching;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddScoped<IUgService, UgService>();

builder.Services.AddScoped<IUsService, UsService>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//builder.Services.AddDbContext<ApplicationDbContext>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});
// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

    await db.Database.EnsureCreatedAsync();

    //await InitializingDb.Initialize(db);
}

app.Run();
