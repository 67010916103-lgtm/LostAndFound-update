using Microsoft.EntityFrameworkCore;
using LostAndFound.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Item}/{action=Index}/{id?}");

// ทำการหุ้ม try-catch ป้องกันโปรแกรมดับเมื่อยังไม่ได้เชื่อมต่อ Database บน Cloud
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<LostAndFound.Data.ApplicationDbContext>();
        if (context.Database.CanConnect() && !context.Categories.Any())
        {
            context.Categories.AddRange(
                new LostAndFound.Models.Category { Name = "อุปกรณ์อิเล็กทรอนิกส์" },
                new LostAndFound.Models.Category { Name = "เอกสาร / กระเป๋าสตางค์" },
                new LostAndFound.Models.Category { Name = "เสื้อผ้า / เครื่องแต่งกาย" },
                new LostAndFound.Models.Category { Name = "กุญแจ" },
                new LostAndFound.Models.Category { Name = "อื่นๆ" }
            );
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        // หากเชื่อมต่อฐานข้อมูลไม่ได้ ให้ข้ามไปรันแอปตามปกติ
        Console.WriteLine($"Database initialization bypassed: {ex.Message}");
    }
}

app.Run();