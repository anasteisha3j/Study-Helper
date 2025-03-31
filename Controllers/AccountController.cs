using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using StudyApp.Models;
using StudyApp.Data;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;


    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    // GET: Fetch all users
    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var users = _context.Users.ToList();
        Console.WriteLine($"Total users found: {users.Count}");
        var users2 = _userManager.Users.Select(u => new { u.Id, u.UserName, u.Email, u.FullName }).ToList();
        Console.WriteLine($"Total users found: {users2.Count}");
        return Ok(users.Select(u => new { u.Id, u.UserName, u.Email, u.FullName }));
    }

    // Register (GET)
    public IActionResult Register() => View();

    // Register (POST)
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        Console.WriteLine($"Entered");

        var user = new User { UserName = model.Email, Email = model.Email, FullName = model.FullName };
        Console.WriteLine($"User_id: {user.Id}, username:{user.UserName}");
        var result = await _userManager.CreateAsync(user, model.Password);
        Console.WriteLine($"User Manager: {_userManager}");

        if (result.Succeeded)
        {
            Console.WriteLine($"Registration successful");
            await _signInManager.SignInAsync(user, isPersistent: false);

            // Debug: Check if user is in the database
            var checkUser = await _userManager.FindByEmailAsync(model.Email);
            if (checkUser != null)
            {
                Console.WriteLine($"User {checkUser.Email} was added!");
            }
            else
            {
                Console.WriteLine("User was not found in the database.");
            }

            return RedirectToAction("Index", "Home");
        }

        Console.WriteLine($"Not successful operation");
        Console.WriteLine($"Success: {result.Succeeded}");
        foreach (var error in result.Errors)
    {
        Console.WriteLine($"Code: {error.Code}, Description: {error.Description}");
    }

        Console.WriteLine($"over");
        return View(model);
    }


    // Login (GET)
    public IActionResult Login() => View();

    // Login (POST)
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        ModelState.AddModelError("", "Invalid login attempt.");
        return View(model);
    }

    // Logout
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
