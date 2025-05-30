using BookIT.WebApp.Application.Services.Interfaces;
using BookIT.WebApp.ViewModels;
using Newtonsoft.Json;

namespace BookIT.WebApp.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserProfileService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ProfileViewModel> GetProfileAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");
            var response = await client.GetAsync($"auth/api/user/profile/{userId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Ошибка получения профиля");

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProfileViewModel>(json);
        }
    }

}
