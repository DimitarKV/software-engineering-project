using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HotelReservations.Data.Entities
{
    public class User : Entity<int> 
    {
        //Identity propperties
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        
        //Mandatory for all user types
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? IsAdult { get; set; }
        
        //Mandatory for Hotel employees
        public Employee? EmployeeProperties { get; set; }
    }
}
