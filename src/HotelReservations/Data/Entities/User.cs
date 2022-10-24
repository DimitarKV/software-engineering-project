using Microsoft.AspNetCore.Identity;

namespace HotelReservations.Data.Entities
{
    public class User : Entity<int>
    {
        //Identity propperties
        public string Username { get; set; }
        public string Password { get; set; }
        
        //Mandatory for all user types
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsAdult { get; set; }
        
        //Mandatory for Hotel employees
        public Employee EmployeeProperties { get; set; }
    }
}
