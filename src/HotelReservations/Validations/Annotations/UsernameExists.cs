using System.ComponentModel.DataAnnotations;
using HotelReservations.Services.UserService.Interfaces;

namespace HotelReservations.Validations.Annotations;

public class UsernameExists : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is null) return ValidationResult.Success;
        var username = value.ToString();
        if (username is null) return ValidationResult.Success;

        var userService = (IUserService) validationContext.GetService(typeof(IUserService))!;
        
        return userService.UserExists(username) ? ValidationResult.Success : new ValidationResult("No such user with the given username exists!");
    }
}