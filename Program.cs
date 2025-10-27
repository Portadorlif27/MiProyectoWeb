using FinanTech.Data;
using FinanTech.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=FinanTech.db"));

var app = builder.Build();

// Middleware
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapHub<NotificationHub>("/hubs/notifications");

// Ensure DB and seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Accounts.Any())
    {
        db.Accounts.AddRange(new[]
        {
            new FinanTech.Models.Account { AccountId = "acct-1001", Owner = "Héctor Ramos", Balance = 1500m },
            new FinanTech.Models.Account { AccountId = "acct-1002", Owner = "Samara S.", Balance = 320.5m }
        });
        db.SaveChanges();
        Console.WriteLine("Seeded sample accounts.");
    }
}

app.Run();
