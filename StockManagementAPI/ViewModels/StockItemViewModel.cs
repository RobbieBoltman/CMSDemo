using StockManagementAPI.Repositories.DataModels;

namespace StockManagementAPI.ViewModels
{
    public class StockItemViewModel
    {
        public int Id { get; set; }
        public string RegNo { get; set; } = null!;
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int ModelYear { get; set; }
        public int Kms { get; set; }
        public string Colour { get; set; } = null!;
        public string Vin { get; set; } = null!;
        public decimal RetailPrice { get; set; }
        public decimal CostPrice { get; set; }
        public DateTime Dtcreated { get; set; }
        public DateTime Dtupdated { get; set; }
        public virtual List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();
        public virtual List<StockAccessoryViewModel> StockAccessories { get; set; } = new List<StockAccessoryViewModel>();
    }
}
