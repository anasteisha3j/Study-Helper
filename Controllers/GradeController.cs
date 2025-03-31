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
    var grades = _context.Grades.ToList(); // або будь-яке джерело даних
    return View(grades);
}


        [HttpGet]
        public IActionResult Create()
        {
            return View(new GradeModel { Date = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeModel gradeModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(gradeModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Помилка збереження: {ex.Message}");
                }
            }
            return View(gradeModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return NotFound();
            
            return View(grade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GradeModel gradeModel)
        {
            if (id != gradeModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradeModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(gradeModel.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(gradeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return NotFound();

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}