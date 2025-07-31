namespace VehicleLeasingApp.Models
{
    public class VehicleReportViewModel
    {
        public string Manufacturer { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}