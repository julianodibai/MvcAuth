using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcAuth.Models;
using MvcAuth.Repositories;
using MvcAuth.Services;
using System.Security.Claims;

namespace MvcAuth.Controllers;

public class HomeController : Controller
{

    [AllowAnonymous]
    public IActionResult Index(bool erroLogin)
    {
        if (erroLogin)
            ViewBag.Erro = "Invalid username or password";

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(User model)
    {
        var user = UserRepositorycs.Get(model.Username, model.Password);

        if (user == null)
            return RedirectToAction("Index", new { erroLogin = true });

        await UserService.Login(user, HttpContext);

        return RedirectToAction("Profile");

    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await UserService.Logout(HttpContext);

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "manager")]
    public IActionResult Profile()
    {
        ViewBag.Permissoes = HttpContext.User.Claims
                                        .Where(c => c.Type == ClaimTypes.Role)
                                        .Select(x => x.Value);                                    

        return View();
    }
}
