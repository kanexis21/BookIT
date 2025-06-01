using BookIT.WebApp.ViewModels.Support;

namespace BookIT.WebApp.Application.Services.Interfaces
{
    public interface ISupportService
    {
        Task<IEnumerable<SupportChatViewModel>> GetActiveSupportChatsAsync();
        Task<IEnumerable<SupportMessageViewModel>> GetMessagesWithUserAsync(string userId);
        Task SendSupportMessageAsync(SupportMessageViewModel message);
        Task<Guid> SendMessageAsync(SupportMessageViewModel message);
        Task<IEnumerable<SupportMessageViewModel>> GetMessagesAsync(Guid user1, Guid user2);
    }
}
