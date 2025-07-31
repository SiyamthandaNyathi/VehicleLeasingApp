namespace VehicleLeasingApp.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public int DriverId { get; set; }
        public Driver Driver { get; set; } = null!;
    }
}