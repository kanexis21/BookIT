using BookIT.WebApp.Application.Services.Interfaces;
using BookIT.WebApp.ViewModels.Support;
using System.Net.Http;

namespace BookIT.WebApp.Application.Services
{
    public class SupportServiceClient : ISupportService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SupportServiceClient(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }
        public async Task<Guid> SendMessageAsync(SupportMessageViewModel message)
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");
            var response = await client.PostAsJsonAsync("support/api/chat/send", message);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<IEnumerable<SupportMessageViewModel>> GetMessagesAsync(Guid user1, Guid user2)
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");
            var response = await client.GetAsync($"support/api/chat/messages?user1={user1}&user2={user2}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<SupportMessageViewModel>>();
        }
        public async Task<IEnumerable<SupportChatViewModel>> GetActiveSupportChatsAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");
            var response = await client.GetAsync("support/api/chats");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<SupportChatViewModel>>();
        }

        public async Task<IEnumerable<SupportMessageViewModel>> GetMessagesWithUserAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");
            var response = await client.GetAsync($"support/api/messages/{userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<SupportMessageViewModel>>();
        }

        public async Task SendSupportMessageAsync(SupportMessageViewModel message)
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");

            var response = await client.PostAsJsonAsync("support/api/chat/messages/send", new
            {
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                SenderName = "Вадим",
                Text = message.Text,
                Timestamp = DateTime.UtcNow
            });
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("❌ Ошибка отправки сообщения: " + content);
            }
            response.EnsureSuccessStatusCode();
        }

    }
}
