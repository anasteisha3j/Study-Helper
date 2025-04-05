using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data; 
using StudyApp.Models; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
namespace StudyApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }
        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                TaskCount = await _context.Tasks.CountAsync(t => t.UserId == GetCurrentUserId()),
                GradeCount = await _context.Grades.CountAsync(g => g.UserId == GetCurrentUserId()),
                NoteCount = await _context.Notes.CountAsync(n => n.UserId == GetCurrentUserId())
            };
            
            return View(model);
        }

        public async Task<IActionResult> Tasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return View(tasks); 
        }

        public async Task<IActionResult> Grades()
        {
            var grades = await _context.Grades.ToListAsync();
            return View(grades);
        }

        public async Task<IActionResult> Notes()
        {
            var notes = await _context.Notes.ToListAsync();
            return View(notes);
        }
<<<<<<< Updated upstream

=======
                private string GetCurrentUserId()
        {
            return User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }
    }
        public class DashboardViewModel
    {
        public int TaskCount { get; set; }
        public int GradeCount { get; set; }
        public int NoteCount { get; set; }
>>>>>>> Stashed changes
    }
}
