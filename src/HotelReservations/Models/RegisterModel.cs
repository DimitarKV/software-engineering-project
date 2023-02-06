using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HotelReservations.Validations.Annotations;

namespace HotelReservations.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Username is required")]
    [UsernameAvailable]
    [DisplayName("Username: ")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DisplayName("Password: ")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Please repeat the password")]
    [DisplayName("Repeat password: ")]
    public string RepeatPassword { get; set; }
    
    public string? Role { get; set; }
    
    [Required(ErrorMessage = "FirstName is required")]
    [DisplayName("First name: ")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required")]
    [DisplayName("Last name: ")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Phone number is required")]
    [DisplayName("Phone number: ")]
    public string PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [DisplayName("Email: ")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Date of birth is required")]
    [DisplayName("Date of birth: ")]
    public DateTime DateOfBirth { get; set; }
}