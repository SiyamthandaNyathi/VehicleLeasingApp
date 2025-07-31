using System.Linq;

namespace VehicleLeasingApp.Models
{
    public static class DbInitializer
    {
        public static void Initialize(VehicleLeasingContext context)
        {
            context.Database.EnsureCreated();

            if (context.Suppliers.Any())
                return; // DB has been seeded

            var suppliers = new[]
            {
                new Supplier { Name = "AutoSupplies Ltd", ContactInfo = "autosupplies@example.com" },
                new Supplier { Name = "CarDepot", ContactInfo = "cardepot@example.com" }
            };
            context.Suppliers.AddRange(suppliers);

            var branches = new[]
            {
                new Branch { Name = "Central", Location = "Downtown" },
                new Branch { Name = "North", Location = "Uptown" }
            };
            context.Branches.AddRange(branches);

            var clients = new[]
            {
                new Client { Name = "Acme Corp", ContactInfo = "acme@example.com" },
                new Client { Name = "Beta LLC", ContactInfo = "beta@example.com" }
            };
            context.Clients.AddRange(clients);

            var drivers = new[]
            {
                new Driver { Name = "John Doe", LicenseNumber = "D123456" },
                new Driver { Name = "Jane Smith", LicenseNumber = "S654321" }
            };
            context.Drivers.AddRange(drivers);

            context.SaveChanges();

            var vehicles = new[]
            {
                new Vehicle {
                    Manufacturer = "Toyota", Model = "Corolla", Year = 2020,
                    SupplierId = suppliers[0].Id, BranchId = branches[0].Id, ClientId = clients[0].Id, DriverId = drivers[0].Id
                },
                new Vehicle {
                    Manufacturer = "Ford", Model = "Focus", Year = 2021,
                    SupplierId = suppliers[1].Id, BranchId = branches[1].Id, ClientId = clients[1].Id, DriverId = drivers[1].Id
                }
            };
            context.Vehicles.AddRange(vehicles);

            context.SaveChanges();
        }
    }
}