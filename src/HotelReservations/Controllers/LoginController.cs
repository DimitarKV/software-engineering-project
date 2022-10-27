using HotelReservations.Models;
using HotelReservations.Services.Security.Interfaces;
using HotelReservations.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public LoginController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

            
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]    
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }
            
            var result = _userService.ValidateUser(Response, viewModel);
            if (result is null)
            {
                ModelState.AddModelError("Password", "Incorrect password");
                return View("Index", viewModel);
            }
            
            var token = _tokenService.BuildToken(result);
            Response.HttpContext.Session.SetString("Token", token);
            
            return Redirect("/Home");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}