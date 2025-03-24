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
        public async Task<IActionResult> Index()
        {
            var grades = await _context.Grades.ToListAsync();  
            if (grades == null)
            {
                return View(new List<GradeModel>());  
            }
            return View(grades);
        }


        public IActionResult Create()
        {
            return View(new GradeModel());
        }

        // [HttpPost]
        // public async Task<IActionResult> Create(GradeModel grade)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Grades.Add(grade);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(grade);
        // }
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(GradeModel grade)
{
    if (!ModelState.IsValid)
    {
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Validation error: {error.ErrorMessage}");
        }
        return View(grade);
    }

    _context.Grades.Add(grade);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}


        public async Task<IActionResult> Edit(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GradeModel grade)
        {
            if (ModelState.IsValid)
            {
                _context.Grades.Update(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
