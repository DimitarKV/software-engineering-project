using HotelReservations.Data.Repositories.Interfaces;
using HotelReservations.Globals;
using HotelReservations.Models;
using HotelReservations.Services.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;

        public LoginController(ILogger<LoginController> logger, IUserRepository userRepository,
            ITokenService tokenService, IConfiguration config, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;
            _httpContext = httpContext;
        }

            
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]    
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            
            if (string.IsNullOrEmpty(viewModel.Username) || string.IsNullOrEmpty(viewModel.Password))
            {
                return (RedirectToAction("Error"));
            }

            var validUser = _userRepository.ValidateAndGetUser(viewModel);

            if (validUser is not null)
            {
                var generatedToken =
                    _tokenService.BuildToken(validUser);
                if (generatedToken is not null)
                {
                    Response.Cookies.Append("Token", generatedToken, new CookieOptions()
                    {
                        Expires = DateTimeOffset.Now.AddMinutes(GlobalVariables.ExpiryDurationMinutes)
                    });
                    return Redirect("/Home");
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            else
            {
                return (RedirectToAction("Error"));
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}