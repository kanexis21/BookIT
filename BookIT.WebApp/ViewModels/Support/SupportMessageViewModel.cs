namespace BookIT.WebApp.ViewModels.Support
{
    public class SupportMessageViewModel
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
