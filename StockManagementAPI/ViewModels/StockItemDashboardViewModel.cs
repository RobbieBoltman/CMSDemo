using StockManagementAPI.Repositories.DataModels;

namespace StockManagementAPI.ViewModels
{
    public class StockItemDashboardViewModel
    {
        public int Id { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int ModelYear { get; set; }
        public int Kms { get; set; }
        public decimal RetailPrice { get; set; }
        public DateTime Dtcreated { get; set; }
        public virtual List<Image> Images { get; set; } = new List<Image>();
    }
}
