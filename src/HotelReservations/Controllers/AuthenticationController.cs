using HotelReservations.Models;
using HotelReservations.Services.Security.Interfaces;
using HotelReservations.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]    
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", viewModel);
            }
            
            var result = _userService.ValidateUser(Response, viewModel);
            if (result is null)
            {
                ModelState.AddModelError("Password", "Incorrect password");
                return View("Login", viewModel);
            }
            
            var token = _tokenService.BuildToken(result);
            Response.HttpContext.Session.SetString("Token", token);
            
            return Redirect("/");
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Logout()
        {
            Response.HttpContext.Session.SetString("Token", "");
            return Redirect("/");
        }
    }
}