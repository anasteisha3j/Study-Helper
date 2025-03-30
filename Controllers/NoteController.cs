using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data;
using StudyApp.Models;

namespace StudyApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Note
        public IActionResult Index()
        {
            return View(_context.Notes.ToList());
        }


        // POST: /Note/Create
[HttpGet]
public IActionResult Create()
{
    return View(new NoteModel()); 
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(NoteModel model) 
{
    if (ModelState.IsValid)
    {
        try
        {
            model.CreatedDate = DateTime.Now;
            _context.Notes.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Помилка збереження: {ex.Message}");
        }
    }
    return View(model);
}


        [HttpGet]
public async Task<IActionResult> Edit(int id)
{
    var note = await _context.Notes.FindAsync(id);
    if (note == null)
    {
        return NotFound();
    }
    return View(note);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Note,CreatedDate")] NoteModel noteModel)
{
    if (id != noteModel.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            var existingNote = await _context.Notes.AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);
                
            if (existingNote != null)
            {
                noteModel.CreatedDate = existingNote.CreatedDate;
            }
            
            noteModel.LastModifiedDate = DateTime.Now;
            _context.Update(noteModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
 
        }
    }
    return View(noteModel);
}


        // POST: /Note/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var note = _context.Notes.Find(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}