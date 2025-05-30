using BookIT.WebApp.ViewModels;

namespace BookIT.WebApp.Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingViewModel>> GetUserBookingsAsync(string userId);
    }

}
