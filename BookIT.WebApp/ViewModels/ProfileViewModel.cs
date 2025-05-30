namespace BookIT.WebApp.ViewModels
{
    public class ProfileViewModel
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<BookingViewModel> Bookings { get; set; } = new();
    }
    public class ProfilePageViewModel
    {
        public ProfileViewModel Profile { get; set; }
        public List<BookingViewModel> Bookings { get; set; }
    }

}
