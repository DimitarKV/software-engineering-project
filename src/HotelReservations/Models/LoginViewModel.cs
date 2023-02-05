using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HotelReservations.Validations.Annotations;

namespace HotelReservations.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        [UsernameExists]
        [DisplayName("Username: ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password: ")]
        public string Password { get; set; }

        [DisplayName("Remember me ")]
        public bool RememberMe { get; set; } = false;

        public string? Message { get; set; } = "";
    }
}
