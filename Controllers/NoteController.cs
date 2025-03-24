using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data;
using StudyApp.Models;
using System.Threading.Tasks;

namespace StudyApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var notes = await _context.Notes.ToListAsync();
            return View(notes); 
        }

        public IActionResult Create()
        {
            return View(new NoteModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create(NoteModel note)
        {
            if (ModelState.IsValid)
            {
                _context.Notes.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(note);
        }

        // public async Task<IActionResult> Edit(int id)
        // {
        //     var note = await _context.Notes.FindAsync(id);
        //     if (note == null) return NotFound();
        //     return View(note);
        // }
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
        public async Task<IActionResult> Edit(NoteModel note)
        {
            if (ModelState.IsValid)
            {
                _context.Notes.Update(note);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(note);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
