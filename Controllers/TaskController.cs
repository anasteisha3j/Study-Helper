// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using StudyApp.Data;
// using StudyApp.Models;

// namespace StudyApp.Controllers
// {
//     public class TaskController : Controller
//     {
//         private readonly ApplicationDbContext _context;

//         public TaskController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<IActionResult> Index()
//         {
//             var tasks = await _context.Tasks.ToListAsync();
//             return View(tasks); 
//         }

//         public IActionResult Create()
//         {
//             return View(new TaskModel()); 
//         }

//         [HttpPost]
//         public async Task<IActionResult> Create(TaskModel task)
//         {
//             if (ModelState.IsValid) 
//             {
//                 _context.Tasks.Add(task); 
//                 await _context.SaveChangesAsync(); 
//                 return RedirectToAction("Index"); 
//             }

//             return View(task); 
//         }

//         public async Task<IActionResult> Edit(int id)
//         {
//             var task = await _context.Tasks.FindAsync(id); 
//             if (task == null)
//             {
//                 return NotFound(); 
//             }

//             return View(task); 
//         }

//         [HttpPost]
//         public async Task<IActionResult> Edit(TaskModel task)
//         {
//             if (ModelState.IsValid) 
//             {
//                 _context.Tasks.Update(task); 
//                 await _context.SaveChangesAsync(); 
//                 return RedirectToAction("Index"); 
//             }

//             return View(task); 
//         }

//         public async Task<IActionResult> Delete(int id)
//         {
//             var task = await _context.Tasks.FindAsync(id); 
//             if (task == null)
//             {
//                 return NotFound(); 
//             }

//             return View(task); 
//         }

//         [HttpPost, ActionName("Delete")]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var task = await _context.Tasks.FindAsync(id); 
//             if (task != null)
//             {
//                 _context.Tasks.Remove(task); 
//                 await _context.SaveChangesAsync(); 
//             }

//             return RedirectToAction("Index"); 
//         }

//         // GET: /Task/MarkCompleted/{id}
//         public async Task<IActionResult> MarkCompleted(int id)
//         {
//             var task = await _context.Tasks.FindAsync(id); 
//             if (task != null)
//             {
//                 task.IsCompleted = true; 
//                 await _context.SaveChangesAsync(); 
//             }

//             return RedirectToAction("Index"); 
//         }
//     }
// }


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

        // GET: /Task/Edit/5
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.Find(id);
            return task == null ? NotFound() : View(task);
        }

        // POST: /Task/Edit/5
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

        // POST: /Task/Delete/5
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

        // POST: /Task/MarkCompleted/5
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