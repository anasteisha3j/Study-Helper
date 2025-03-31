using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data; 
using StudyApp.Models; 

namespace StudyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Tasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return View(tasks); 
        }

        public async Task<IActionResult> Grades()
        {
            var grades = await _context.Grades.ToListAsync();
            return View();
        }

        public IActionResult Notes()
        {
            return RedirectToAction("Index", "Note");
        }
    }
}
