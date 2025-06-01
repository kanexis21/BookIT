using BookIT.WebApp.Application.Services.Interfaces;
using BookIT.WebApp.ViewModels;
using BookIT.WebApp.ViewModels.Catalog.Resource;
using BookIT.WebApp.ViewModels.Catalog.Room;
using Newtonsoft.Json;
using System.Text;

namespace BookIT.WebApp.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<BookingViewModel>> GetAllBookingsAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");

            var response = await client.GetAsync("booking/api/booking");
            if (!response.IsSuccessStatusCode)
                return new();

            var bookingsJson = await response.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<List<BookingViewModel>>(bookingsJson);

            var resourceIds = bookings
                .Where(b => b.ResourceId.HasValue)
                .Select(b => b.ResourceId!.Value)
                .Distinct()
                .ToList();

            var roomIds = bookings
                .Where(b => b.RoomId.HasValue)
                .Select(b => b.RoomId!.Value)
                .Distinct()
                .ToList();

            if (resourceIds.Any())
            {
                var requestContent = new StringContent(JsonConvert.SerializeObject(resourceIds), Encoding.UTF8, "application/json");
                var resourceResponse = await client.PostAsync("resource/api/resource/by-ids", requestContent);

                if (resourceResponse.IsSuccessStatusCode)
                {
                    var resourceJson = await resourceResponse.Content.ReadAsStringAsync();
                    var resources = JsonConvert.DeserializeObject<List<ResourceShortViewModel>>(resourceJson);

                    foreach (var booking in bookings)
                    {
                        if (booking.ResourceId.HasValue)
                            booking.ResourceName = resources.FirstOrDefault(r => r.Id == booking.ResourceId)?.Name ?? "Неизвестный ресурс";
                    }
                }
            }

            if (roomIds.Any())
            {
                var requestContent = new StringContent(JsonConvert.SerializeObject(roomIds), Encoding.UTF8, "application/json");
                var roomResponse = await client.PostAsync("room/api/room/by-ids", requestContent);

                if (roomResponse.IsSuccessStatusCode)
                {
                    var roomJson = await roomResponse.Content.ReadAsStringAsync();
                    var rooms = JsonConvert.DeserializeObject<List<RoomShortViewModel>>(roomJson);

                    foreach (var booking in bookings)
                    {
                        if (booking.RoomId.HasValue)
                            booking.RoomName = rooms.FirstOrDefault(r => r.Id == booking.RoomId)?.Name ?? "Неизвестное помещение";
                    }
                }
            }

            return bookings;
        }


        public async Task<List<BookingViewModel>> GetUserBookingsAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");

            var bookingsResponse = await client.GetAsync($"booking/api/booking/my?userId={userId}");
            if (!bookingsResponse.IsSuccessStatusCode)
                return new();

            var bookingsJson = await bookingsResponse.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<List<BookingViewModel>>(bookingsJson);

            var resourceIds = bookings
                .Where(b => b.ResourceId.HasValue)
                .Select(b => b.ResourceId!.Value)
                .Distinct()
                .ToList();

            var roomIds = bookings
                .Where(b => b.RoomId.HasValue)
                .Select(b => b.RoomId!.Value)
                .Distinct()
                .ToList();

            if (resourceIds.Any())
            {
                var requestContent = new StringContent(JsonConvert.SerializeObject(resourceIds), Encoding.UTF8, "application/json");
                var resourceResponse = await client.PostAsync("resource/api/resource/by-ids", requestContent);

                if (resourceResponse.IsSuccessStatusCode)
                {
                    var resourceJson = await resourceResponse.Content.ReadAsStringAsync();
                    var resources = JsonConvert.DeserializeObject<List<ResourceShortViewModel>>(resourceJson);

                    foreach (var booking in bookings)
                    {
                        if (booking.ResourceId.HasValue)
                            booking.ResourceName = resources.FirstOrDefault(r => r.Id == booking.ResourceId)?.Name ?? "Неизвестный ресурс";
                    }
                }
            }

            if (roomIds.Any())
            {
                var requestContent = new StringContent(JsonConvert.SerializeObject(roomIds), Encoding.UTF8, "application/json");
                var roomResponse = await client.PostAsync("room/api/room/by-ids", requestContent); 

                if (roomResponse.IsSuccessStatusCode)
                {
                    var roomJson = await roomResponse.Content.ReadAsStringAsync();
                    var rooms = JsonConvert.DeserializeObject<List<RoomShortViewModel>>(roomJson);

                    foreach (var booking in bookings)
                    {
                        if (booking.RoomId.HasValue)
                            booking.RoomName = rooms.FirstOrDefault(r => r.Id == booking.RoomId)?.Name ?? "Неизвестное помещение";
                    }
                }
            }

            return bookings;
        }
    }

}
