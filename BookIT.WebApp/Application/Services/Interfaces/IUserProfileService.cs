using BookIT.WebApp.ViewModels;

namespace BookIT.WebApp.Application.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<ProfileViewModel> GetProfileAsync(string userId);
    }

}
