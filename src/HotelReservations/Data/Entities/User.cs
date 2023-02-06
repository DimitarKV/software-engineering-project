using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HotelReservations.Data.Entities
{
    public class User : IdentityUser<int> 
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? IsAdult { get; set; }
        
        //Mandatory for Hotel employees
        public Employee? EmployeeProperties { get; set; }

        public IEnumerable<Hotel> Hotels { get; set; }
    }
}
