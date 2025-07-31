using Microsoft.EntityFrameworkCore;

namespace VehicleLeasingApp.Models
{
    public class VehicleLeasingContext : DbContext
    {
        public VehicleLeasingContext(DbContextOptions<VehicleLeasingContext> options) : base(options) { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}