using BookIT.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

public class BookingController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    public BookingController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    [HttpGet]
    public IActionResult CreateWithResource(Guid resourceId)
    {
        var model = new BookingViewModel
        {
            ResourceId = resourceId,
            StartTime = DateTime.Now.AddHours(1),
            EndTime = DateTime.Now.AddHours(2)
        };
        return View(model);
    }
    [HttpGet]
    public IActionResult CreateWithRoom(Guid roomId)
    {
        var model = new BookingViewModel
        {
            RoomId = roomId,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(1)
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWithResource(BookingViewModel model)
    {
        var client = _httpClientFactory.CreateClient("ApiGateway");
        var dateString = model.StartTime.ToString("yyyy-MM-dd");

        var response = await client.GetAsync($"booking/api/booking/free-slots-resource?resourceId={model.ResourceId}&date={dateString}");

        if (response.IsSuccessStatusCode)
        {
            var slots = await response.Content.ReadFromJsonAsync<List<TimeSlotViewModel>>();
            model.FreeTimeSlots = slots;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Ошибка получения свободных слотов: {errorContent}");
        }

        var json = JsonConvert.SerializeObject(model);
        Console.WriteLine(json);

        response = await client.PostAsJsonAsync("booking/api/booking", model);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Ошибка создания бронирования: {errorContent}");
            ModelState.AddModelError("", "Ошибка при создании бронирования.");
            return View(model);
        }

        return RedirectToAction("Index", "Catalog");
    }

    [HttpPost]
    public async Task<IActionResult> CreateWithRoom(BookingViewModel model)
    {
        var client = _httpClientFactory.CreateClient("ApiGateway");
        var dateString = model.StartTime.ToString("yyyy-MM-dd");

        var response = await client.GetAsync($"booking/api/booking/free-slots-room?roomId={model.RoomId}&date={dateString}");

        if (response.IsSuccessStatusCode)
        {
            var slots = await response.Content.ReadFromJsonAsync<List<TimeSlotViewModel>>();
            model.FreeTimeSlots = slots;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Ошибка получения свободных слотов: {errorContent}");
        }

        var json = JsonConvert.SerializeObject(model);
        Console.WriteLine(json);

        response = await client.PostAsJsonAsync("booking/api/booking", model);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Ошибка создания бронирования: {errorContent}");
            ModelState.AddModelError("", "Ошибка при создании бронирования.");
            return View(model);
        }

        return RedirectToAction("Index", "Catalog");
    }
}
