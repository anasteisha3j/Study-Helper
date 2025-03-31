using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data;
using StudyApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.Controllers
{
    [Authorize] // Ensure only authenticated users can access notes
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public NoteController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Console.WriteLine("Unauthorized access attempt!");
                return RedirectToAction("Login", "Account");
            }

            _context.ChangeTracker.Clear();
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                Console.WriteLine("User is NULL in Index action.");
                return RedirectToAction("Login", "Account");
            }

            Console.WriteLine($"User Retrieved: {user.Email}, {user.Id}");
            Console.WriteLine($"IsAuthenticated: {User.Identity.IsAuthenticated}");

            var notes = await _context.Notes
                .Where(n => n.UserId == user.Id)
                .ToListAsync();

            Console.WriteLine($"Total notes found: {notes.Count}");

            return View(notes);
        }

        // GET: /Note/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new NoteModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteModel model)
        {
            Console.WriteLine("Create method triggered.");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return RedirectToAction("Login", "Account");
            }

            Console.WriteLine($"User: {user.Id}");

            model.UserId = user.Id;

            model.Author = user.UserName; // Or any other user property

            model.CreatedDate = DateTime.Now;

            Console.WriteLine($"{model.UserId} {model.Author}");

            Console.WriteLine($"Saving note for user: {user.Id}");

            _context.Notes.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Note/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == user.Id); // Ensure the note belongs to the user

            if (note == null) return NotFound();

            return View(note);
        }

        // POST: /Note/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Note")] NoteModel noteModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var existingNote = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == user.Id);

            if (existingNote == null) return NotFound();

            if (ModelState.IsValid)
            {
                existingNote.Title = noteModel.Title;
                existingNote.Note = noteModel.Note;
                existingNote.LastModifiedDate = DateTime.Now;

                _context.Update(existingNote);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(noteModel);
        }

        // POST: /Note/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == user.Id);

            if (note == null) return NotFound();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
