using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StudyApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Додаємо сервіси до контейнера
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=studyapp.db"));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Налаштування HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Налаштування обробки статичних файлів
app.UseStaticFiles();

// Дозволяємо перегляд вмісту папки uploads (тільки для розробки)
if (app.Environment.IsDevelopment())
{
    var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    if (!Directory.Exists(uploadsPath))
    {
        Directory.CreateDirectory(uploadsPath);
    }
    
    app.UseDirectoryBrowser(new DirectoryBrowserOptions
    {
        FileProvider = new PhysicalFileProvider(uploadsPath),
        RequestPath = "/uploads"
    });
}

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Use(async (context, next) => 
{
    context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
    await next();
});
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();