
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data;
using StudyApp.Models;

[Authorize]
public class TaskController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public TaskController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: /Task
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var tasks = await _context.Tasks
            .Where(t => t.UserId == user.Id)
            .OrderBy(t => t.Deadline)
            .ToListAsync();

        return View(tasks);
    }

    // GET: /Task/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View(new TaskModel 
        {
            Deadline = DateTime.Now.AddDays(1) // Set default deadline
        });
    }

        // POST: /Task/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Set required fields BEFORE validation
        model.UserId = user.Id;
        model.Author = user.UserName;
        model.IsCompleted = false;

        // Clear ModelState errors for fields we set programmatically
        ModelState.Remove("UserId");
        ModelState.Remove("Author");

        if (ModelState.IsValid)
        {
            _context.Tasks.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
    // POST: /Task/MarkCompleted
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkCompleted(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

        if (task != null)
        {
            task.IsCompleted = true;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    // POST: /Task/Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }


   // GET: /Task/Edit/5
[HttpGet]
public async Task<IActionResult> Edit(int id)
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var task = await _context.Tasks
        .FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);
        
    return task == null ? NotFound() : View(task);
}

// POST: /Task/Edit/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, TaskModel taskModel)
{
    if (id != taskModel.Id) 
    {
        return NotFound();
    }

    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return RedirectToAction("Login", "Account");
    }

    // Verify the task belongs to the current user
    var existingTask = await _context.Tasks
        .FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);
        
    if (existingTask == null)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            // Update only the fields that should be editable
            existingTask.Title = taskModel.Title;
            existingTask.Description = taskModel.Description;
            existingTask.Deadline = taskModel.Deadline;
            existingTask.IsCompleted = taskModel.IsCompleted;

            _context.Update(existingTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TaskExists(taskModel.Id))
            {
                return NotFound();
            }
            throw;
        }
    }
    return View(taskModel);
}

private bool TaskExists(int id)
{
    return _context.Tasks.Any(e => e.Id == id);
}
}