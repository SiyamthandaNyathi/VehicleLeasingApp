using System.Collections.Generic;

namespace VehicleLeasingApp.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}