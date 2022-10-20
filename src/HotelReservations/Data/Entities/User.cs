namespace HotelReservations.Data.Entities
{
    public class User : Entity<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime RecruitDate { get; set; }
        public bool isActive { get; set; }
        public DateTime DateOfRelease { get; set; }
    }
}
