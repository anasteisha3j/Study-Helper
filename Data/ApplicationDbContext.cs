using Microsoft.EntityFrameworkCore;
using StudyApp.Models;

namespace StudyApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<GradeModel> Grades { get; set; }
        public DbSet<NoteModel> Notes { get; set; }

    }
}
