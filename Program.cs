<<<<<<< Updated upstream
using Microsoft.AspNetCore.StaticFiles;
=======
using Microsoft.AspNetCore.Identity;
>>>>>>> Stashed changes
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StudyApp.Data;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< Updated upstream
// Додаємо сервіси до контейнера
=======
>>>>>>> Stashed changes
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=studyapp.db"));  

<<<<<<< Updated upstream
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
=======
builder.Services.AddIdentity<User, IdentityRole>(options => 
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();



builder.Services.AddControllersWithViews();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});
var app = builder.Build();

// Middleware Pipeline
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

=======
app.UseAuthentication();
>>>>>>> Stashed changes
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
<<<<<<< Updated upstream
app.Use(async (context, next) => 
{
    context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
    await next();
});
using (var scope = app.Services.CreateScope())
=======

try 
>>>>>>> Stashed changes
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); 
    Console.WriteLine("Database migrated successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
}

app.Run();