using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data;
using StudyApp.Models;
using System.IO;

namespace StudyApp.Controllers
{
    public class StudyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public StudyController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Study
        public async Task<IActionResult> Index()
        {
            return View(await _context.Studies.ToListAsync());
        }

        // GET: Study/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Study/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content")] Study study, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    study.FilePath = "/uploads/" + uniqueFileName;
                }

                study.CreatedDate = DateTime.Now;
                _context.Add(study);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(study);
        }

  

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, Study study, IFormFile uploadedFile)
{
    if (id != study.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            var existingStudy = await _context.Studies.FindAsync(id);
            
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                // Видалити старий файл, якщо він існує
                if (!string.IsNullOrEmpty(existingStudy.FilePath))
                {
                    DeleteFile(existingStudy.FilePath);
                }
                existingStudy.FilePath = await SaveFile(uploadedFile);
            }
            
            existingStudy.Title = study.Title;
            existingStudy.Content = study.Content;
            existingStudy.LastModifiedDate = DateTime.Now;
            
            _context.Update(existingStudy);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudyExists(study.Id))
            {
                return NotFound();
            }
            throw;
        }
        return RedirectToAction(nameof(Index));
    }
    return View(study);
}

[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var study = await _context.Studies.FindAsync(id);
    if (study != null)
    {
        if (!string.IsNullOrEmpty(study.FilePath))
        {
            DeleteFile(study.FilePath);
        }
        _context.Studies.Remove(study);
        await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
}

private async Task<string> SaveFile(IFormFile file)
{
    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    if (!Directory.Exists(uploadsFolder))
    {
        Directory.CreateDirectory(uploadsFolder);
    }

    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

    using (var fileStream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(fileStream);
    }

    return "/uploads/" + uniqueFileName;
}

private void DeleteFile(string filePath)
{
    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));
    if (System.IO.File.Exists(fullPath))
    {
        System.IO.File.Delete(fullPath);
    }
}
        private bool StudyExists(int id)
        {
            return _context.Studies.Any(e => e.Id == id);
        }
    }
}