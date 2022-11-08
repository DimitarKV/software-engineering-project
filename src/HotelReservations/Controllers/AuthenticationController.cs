using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManger;

        public AuthenticationController(IMapper mapper, UserManager<User> userManager,
            SignInManager<User> signInManager, RoleManager<Role> roleManger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManger = roleManger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Password != viewModel.RepeatPassword)
                {
                    ModelState.AddModelError("RepeatPassword", "Passwords don't match!");
                    return View("Register", viewModel);
                }

                var user = _mapper.Map<User>(viewModel);
                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    var userRole = await _roleManger.FindByNameAsync("User");
                    if (userRole is null)
                    {
                        await _roleManger.CreateAsync(new Role() {Name = "User"});
                        userRole = await _roleManger.FindByNameAsync("User");
                    }

                    await _userManager.AddToRoleAsync(user, userRole.Name);

                    await _signInManager.SignInAsync(user, true);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Register Attempt");
            }

            return View("Register", viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //Add remember me button and substitute it with the hardcoded true value in the method bellow
                var result =
                    await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}