using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MvcAuth.Models;
using System.Security.Claims;

namespace MvcAuth.Services
{
    public static class UserService
    {
        public static async Task Login(User user, HttpContext httpContext)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(ClaimTypes.Role, user.Role));

            var claimIdentity = new ClaimsPrincipal(
                      new ClaimsIdentity(
                          claims,
                          CookieAuthenticationDefaults.AuthenticationScheme
                      ));

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.Now.AddHours(8),
                IssuedUtc = DateTime.Now
            };

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimIdentity, authProperties);
        }

        public static async Task Logout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
