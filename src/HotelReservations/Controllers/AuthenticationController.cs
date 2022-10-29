using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.Models;
using HotelReservations.Services.Security.Interfaces;
using HotelReservations.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//TODO: Add password hashing on form submit so none of the apps classes know the original pass
namespace HotelReservations.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthenticationController(IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [AllowAnonymous]    
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", viewModel);
            }

            if (viewModel.Password != viewModel.RepeatPassword)
            {
                ModelState.AddModelError("RepeatPassword", "Passwords don't match!");
                return View("Register", viewModel);
            }

            var user = _mapper.Map<User>(viewModel);
            user.Role = "User";
            user = _userService.RegisterUser(user);

            if (user is null)
            {
                return View("Register", viewModel);
            }

            var token = _tokenService.BuildToken(user);
            Response.HttpContext.Session.SetString("Token", token);
            
            return RedirectToAction("", "Home");
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
            
            var result = _userService.ValidateUser(viewModel);
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