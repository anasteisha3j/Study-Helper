using Microsoft.AspNetCore.Mvc;
using studyhelper.Models;

namespace studyhelper.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (user.Username == "admin" && user.Password == "password")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Невірний логін або пароль!";
            return View();
        }
    }
}
