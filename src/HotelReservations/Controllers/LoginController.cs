using HotelReservations.Data.Repositories.Interfaces;
using HotelReservations.Models;
using HotelReservations.Services.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public LoginController(ILogger<LoginController> logger, IUserRepository userRepository,
            ITokenService tokenService, IConfiguration config)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

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
                    _tokenService.BuildToken(_config["Jwt:Key"]
                        , _config["Jwt:Issuer"]
                        , validUser);
                if (generatedToken is not null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
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