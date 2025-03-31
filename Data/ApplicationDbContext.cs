using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyApp.Models;

namespace StudyApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<GradeModel> Grades { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
