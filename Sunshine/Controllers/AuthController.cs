using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Sunshine.Models;
using Sunshine.Repositories;
using Sunshine.Services;
using Sunshine.ViewModels;
using System;
using System.Threading.Tasks;

namespace Sunshine.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthRepository _authRepository;
        private readonly MenusRepository _menusRepository;
        private readonly MailService _mailService;
        private readonly AuthService _authService;
        public AuthController(AuthRepository authRepository, MenusRepository menusRepository, MailService mailService, AuthService authService)
        {
            _authRepository = authRepository;
            _menusRepository = menusRepository;
            _mailService = mailService;
            _authService = authService;
        }

        // GET: Auth/AccessDenied
        public async Task<ActionResult> AccessDenied()
        {
            ViewData["Menus"] = await _menusRepository.GetIncludedSubMenus();

            return View();
        }
        
        // GET: Auth/Login
        public async Task<ActionResult> Login()
        {
            ViewData["Menus"] = await _menusRepository.GetIncludedSubMenus();

            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthLoginViewModel authLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _authRepository.IsValidAuthData(authLoginViewModel);
                if (user != null)
                {
                    if (user.IsConfirmedEmail)
                    {
                        await _authService.Authenticate(HttpContext, user.Email, user.Role);
                        return RedirectToAction(nameof(Index), "News");
                    }
                    string token = _authService.GetConfirmatioEmailToken(user.Email, user.Role);
                    string url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/auth/confirmationEmail/{token}";
                    await _mailService.SendEmailConfirmationEmail(user.Email, url);
                    ModelState.AddModelError("", "Підтвердіть ваш email");
                }
                else
                {
                    ModelState.AddModelError("", "Не правильний email або пароль");
                }
            }
            return View(authLoginViewModel);
        }

        // GET: Auth/Register/5
        public async Task<IActionResult> Register()
        {
            ViewData["Menus"] = await _menusRepository.GetIncludedSubMenus();

            return View();
        }

        // POST: Auth/Register/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AuthRegisterViewModel authRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!_authRepository.IsEmailTaken(authRegisterViewModel))
                {
                    User newUser = await _authRepository.Resigter(authRegisterViewModel);
                    await _mailService.SendEmailConfirmationEmail(newUser.Email, "link!!");
                    TempData["Success"] = "Ви успішно зареєструвалися. Лист з підтвердженням було надіслано на ваш email";
                    return RedirectToAction(nameof(Index), "News");
                }
                else
                {
                    ModelState.AddModelError("", "Користуваач з введеним email уже існує");
                }
            }
            return View(authRegisterViewModel);
        }

        // GET: Auth/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "News");
        }


        // GET: Auth/ConfirmationEmail/token
        public IActionResult ConfirmationEmail(string token)
        {
            return RedirectToAction("Index", "News");
        }
    }   
}
