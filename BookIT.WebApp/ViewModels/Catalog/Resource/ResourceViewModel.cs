namespace BookIT.WebApp.ViewModels.Catalog.Resource
{
    public enum ResourceStatus
    {
        Доступен,
        Недоступен,
        Обслуживается
    }
    public class ResourceViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string? Location { get; set; }
        public string? Description { get; set; }

        public ResourceStatus Status { get; set; }
    }
}
