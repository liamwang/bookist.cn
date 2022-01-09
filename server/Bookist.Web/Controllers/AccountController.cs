using Bookist.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookist.Web.Controllers;

public class AccountController : Controller
{
    public IActionResult Login(string returnUrl = "/admin")
    {
        LoginVM vm = new()
        {
            ReturnUrl = returnUrl
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm, [FromServices] IConfiguration config)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var cfgUsername = config.GetValue<string>("Admin:Username");
        var cfgPassword = config.GetValue<string>("Admin:Password");

        if (cfgUsername.Equals(vm.Username, StringComparison.OrdinalIgnoreCase)
           && cfgPassword.Equals(vm.Password))
        {
            var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, vm.Username),
                    new Claim(ClaimTypes.Name, vm.Username)
                };

            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

            var identity = new ClaimsIdentity(claims, scheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(scheme, principal, new AuthenticationProperties()
            {
                IsPersistent = vm.RememberMe
            });

            return LocalRedirect(vm.ReturnUrl);
        }

        TempData["Message"] = "用户名或密码错误！";

        return View(vm);
    }

    [Authorize]
    public async Task<IActionResult> Logout(string returnUrl = "/")
    {
        await HttpContext.SignOutAsync();
        return LocalRedirect(returnUrl);
    }
}
