// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using StudyApp.Data;
// using StudyApp.Models;

// public static class SeedData
// {
//     public static async Task Initialize(IServiceProvider serviceProvider)
//     {
//         var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
//         var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//         var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//         await context.Database.MigrateAsync();

//         // Seed roles
//         string[] roles = { "Admin", "Teacher", "User" };
//         foreach (var role in roles)
//         {
//             if (!await roleManager.RoleExistsAsync(role))
//             {
//                 await roleManager.CreateAsync(new IdentityRole(role));
//             }
//         }

//         // Seed admin user
//         var adminEmail = "admin@studyapp.com";
//         if (await userManager.FindByEmailAsync(adminEmail) == null)
//         {
//             var adminUser = new ApplicationUser
//             {
//                 UserName = adminEmail,
//                 Email = adminEmail,
//                 FullName = "Адмін",
//                 RegistrationDate = DateTime.Now
//             };
            
//             var result = await userManager.CreateAsync(adminUser, "Admin");
//             if (result.Succeeded)
//             {
//                 await userManager.AddToRoleAsync(adminUser, "Admin");
//             }
//         }

//         // Seed teacher user
//         var teacherEmail = "teacher@studyapp.com";
//         if (await userManager.FindByEmailAsync(teacherEmail) == null)
//         {
//             var teacherUser = new ApplicationUser
//             {
//                 UserName = teacherEmail,
//                 Email = teacherEmail,
//                 FullName = "John Pork",
//                 RegistrationDate = DateTime.Now
//             };
            
//             var result = await userManager.CreateAsync(teacherUser, "timcheese");
//             if (result.Succeeded)
//             {
//                 await userManager.AddToRoleAsync(teacherUser, "Teacher");
//             }
//         }
//     }
// }