using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data;
using StudyApp.Models;

namespace StudyApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Task
        public IActionResult Index()
        {
            return View(_context.Tasks.ToList());
        }

        // GET: /Task/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Task/Create
        [HttpPost]
        public IActionResult Create(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: /Task/Edit
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.Find(id);
            return task == null ? NotFound() : View(task);
        }

        // POST: /Task/Edit
        [HttpPost]
        public IActionResult Edit(int id, TaskModel task)
        {
            if (id != task.Id) return NotFound();
            
            if (ModelState.IsValid)
            {
                _context.Update(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // POST: /Task/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: /Task/MarkCompleted
        [HttpPost]
        public IActionResult MarkCompleted(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                task.IsCompleted = true;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}