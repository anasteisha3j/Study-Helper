// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using StudyApp.Models;
// using StudyApp.Models.AccountViewModels;

// namespace StudyApp.Controllers
// {
//     public class AuthController : Controller
//     {
//         private readonly SignInManager<ApplicationUser> _signInManager;
//         private readonly UserManager<ApplicationUser> _userManager;

//         public AuthController(
//             SignInManager<ApplicationUser> signInManager,
//             UserManager<ApplicationUser> userManager)
//         {
//             _signInManager = signInManager;
//             _userManager = userManager;
//         }

//         // // GET: /Auth/Login
//         // [HttpGet]
//         // public IActionResult Login()
//         // {
//         //     return View();
//         // }

//         // // POST: /Auth/Login
//         // [HttpPost]
//         // public async Task<IActionResult> Login(LoginViewModel model)
//         // {
//         //     if (ModelState.IsValid)
//         //     {
//         //         var result = await _signInManager.PasswordSignInAsync(
//         //             model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

//         //         if (result.Succeeded)
//         //             return RedirectToAction("Index", "Home");

//         //         ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//         //     }
//         //     return View(model);
//         // }

//         // GET: /Auth/Register
//         [HttpGet]
//         public IActionResult Register()
//         {
//             return View();
//         }

//         // POST: /Auth/Register
//         [HttpPost]
//         public async Task<IActionResult> Register(RegisterViewModel model)
//         {
//             if (ModelState.IsValid)
//             {
//                 var user = new ApplicationUser
//                 {
//                     UserName = model.Email,
//                     Email = model.Email,
//                 };

//                 var result = await _userManager.CreateAsync(user, model.Password);

//                 if (result.Succeeded)
//                 {
//                     await _signInManager.SignInAsync(user, isPersistent: false);
//                     return RedirectToAction("Index", "Home");
//                 }

//                 foreach (var error in result.Errors)
//                     ModelState.AddModelError(string.Empty, error.Description);
//             }
//             return View(model);
//         }

//         // POST: /Auth/Logout
//         [HttpPost]
//         public async Task<IActionResult> Logout()
//         {
//             await _signInManager.SignOutAsync();
//             return RedirectToAction("Index", "Home");
//         }


//         [HttpGet]
// public IActionResult Login(string? returnUrl = null)
// {
//     ViewData["ReturnUrl"] = returnUrl;
//     return View(); // Теперь ищет в Views/Auth/Login.cshtml
// }

// [HttpPost]
// public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
// {
//     ViewData["ReturnUrl"] = returnUrl;
    
//     if (ModelState.IsValid)
//     {
//         var result = await _signInManager.PasswordSignInAsync(
//             model.Email, 
//             model.Password, 
//             model.RememberMe, 
//             lockoutOnFailure: false);
        
//         if (result.Succeeded)
//         {
//             return RedirectToLocal(returnUrl);
//         }
        
//         ModelState.AddModelError(string.Empty, "Невірний логін або пароль");
//     }
    
//     return View(model);
// }
//     }
// }


