namespace HotelReservations.ViewModels;

public class UserViewModel
{
    public string UserName { get; set; }
    public IEnumerable<string> Roles { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool? IsAdult { get; set; }
    
    public EmployeeViewModel? EmployeeProperties { get; set; }

}