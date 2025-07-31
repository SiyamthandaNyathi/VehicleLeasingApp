using System.Collections.Generic;

namespace VehicleLeasingApp.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}