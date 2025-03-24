using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data;
using StudyApp.Models;

namespace StudyApp.Controllers
{
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Grades.ToList());

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
public IActionResult Create([Bind("Subject,Grade,Date")] GradeModel grade)        {
            if (ModelState.IsValid)
            {
                _context.Grades.Add(grade);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        public IActionResult Edit(int id)
        {
            var grade = _context.Grades.Find(id);
            return grade == null ? NotFound() : View(grade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, GradeModel grade)
        {
            if (id != grade.Id) return NotFound();
            
            if (ModelState.IsValid)
            {
                _context.Update(grade);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var grade = _context.Grades.Find(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}