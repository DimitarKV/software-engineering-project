using System.ComponentModel.DataAnnotations;
using HotelReservations.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotelReservations.Validations.Annotations;

public class UsernameAvailable : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
         if(value is null) return ValidationResult.Success;
         var username = value.ToString();
         if (username is null) return ValidationResult.Success;
        
         var userService = (UserManager<User>) validationContext.GetService(typeof(UserManager<User>))!;
        
         return userService.FindByNameAsync(username).Result != null ? new ValidationResult("Username already exists!") : ValidationResult.Success;
    }
}