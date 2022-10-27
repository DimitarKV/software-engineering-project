using HotelReservations.Data.Repositories.Interfaces;
using HotelReservations.Services.Security.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.MediatR.Commands;

public class LoginUserCommand : IRequest<bool>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContext;

    public LoginUserCommandHandler(IUserRepository userRepository, ITokenService tokenService, IHttpContextAccessor httpContext)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _httpContext = httpContext;
    }

    public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return false;
        }

        var validUser = await _userRepository.ValidateAndGetUserAsync(request);

        if (validUser is not null)
        {
            var generatedToken =
                _tokenService.BuildToken(validUser);
            if (generatedToken is not null)
            {
                // Response.HttpContext.Session.SetString("Token", generatedToken);
                _httpContext.HttpContext!.Session.SetString("Token", generatedToken);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
