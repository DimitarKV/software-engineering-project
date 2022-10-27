using HotelReservations.Data.Repositories.Interfaces;
using HotelReservations.Globals;
using HotelReservations.MediatR.Commands;
using HotelReservations.Models;
using HotelReservations.Services.Security.Interfaces;
using MediatR;
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
        private readonly IMediator _mediator;

        public LoginController(ILogger<LoginController> logger, IUserRepository userRepository,
            ITokenService tokenService, IConfiguration config, IHttpContextAccessor httpContext, IMediator mediator)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;
            _httpContext = httpContext;
            _mediator = mediator;
        }

            
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]    
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var result = await _mediator.Send(new LoginUserCommand() {Username = viewModel.Username, Password = viewModel.Password});
            if (result)
            {
                return Redirect("/Home");
            }

            return RedirectToAction("Error");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}