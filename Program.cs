using Microsoft.EntityFrameworkCore;
using LostAndFound.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register ApplicationDbContext (เปลี่ยนจาก UseSqlServer มาใช้ UseInMemoryDatabase)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("LostAndFoundDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Item}/{action=Index}/{id?}");

// ทำการสร้างข้อมูลเริ่มต้น (Categories)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<LostAndFound.Data.ApplicationDbContext>();
        context.Database.EnsureCreated(); // สร้างฐานข้อมูลจำลองในแรม

        if (!context.Categories.Any())
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
        Console.WriteLine($"Database initialization bypassed: {ex.Message}");
    }
}

app.Run();