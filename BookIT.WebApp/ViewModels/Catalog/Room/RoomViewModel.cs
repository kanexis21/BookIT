﻿namespace BookIT.WebApp.ViewModels.Catalog.Room
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
    }

}
