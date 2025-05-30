using System.ComponentModel.DataAnnotations;

namespace BookIT.WebApp.ViewModels
{
    public enum BookingStatus
    {
        Забронировано,
        Недоступно,
        Свободно
    }
    public class TimeSlotViewModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
    public class BookingViewModel
    {
        public Guid? Id { get; set; }
        public Guid? ResourceId { get; set; }
        public Guid? RoomId { get; set; }
        public string Message { get; set; }
        public string? RoomName { get; set; }
        public string? ResourceName { get; set; }

        [Display(Name = "Начало бронирования")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Конец бронирования")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Display(Name = "Статус")]
        public BookingStatus? Status { get; set; }

        [Display(Name = "Описание")]
        public string? Description { get; set; }
        public string SelectedSlot { get; set; }
        public List<TimeSlotViewModel>? FreeTimeSlots { get; set; }
    }

}
