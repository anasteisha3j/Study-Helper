// using Microsoft.EntityFrameworkCore;
// using StudyApp.Models;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// namespace StudyApp.Data
// {
//     public class ApplicationDbContext :IdentityDbContext<IdentityUser>
//     {
//         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

//         public DbSet<TaskModel> Tasks { get; set; }
//         public DbSet<GradeModel> Grades { get; set; }
//         public DbSet<NoteModel> Notes { get; set; }
//         public DbSet<User> Users { get; set; }
//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<User>()
//             .HasIndex(u => u.Username)
//             .IsUnique();
//     }


//     }
// }

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyApp.Models;

namespace StudyApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<GradeModel> Grades { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<Study> Studies { get; set; }    }
}