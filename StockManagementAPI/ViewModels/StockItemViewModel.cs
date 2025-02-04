using StockManagementAPI.Repositories.DataModels;

namespace StockManagementAPI.ViewModels
{
    using System.Text.Json.Serialization;

    public class StockItemViewModel
    {
        public int Id { get; set; }

        [JsonPropertyName("regNo")]
        public string RegNo { get; set; } = null!;

        [JsonPropertyName("make")]
        public string Make { get; set; } = null!;

        [JsonPropertyName("model")]
        public string Model { get; set; } = null!;

        [JsonPropertyName("modelYear")]
        public int ModelYear { get; set; }

        [JsonPropertyName("kms")]
        public int Kms { get; set; }

        [JsonPropertyName("colour")]
        public string Colour { get; set; } = null!;

        [JsonPropertyName("vin")]
        public string Vin { get; set; } = null!;

        [JsonPropertyName("retailPrice")]
        public decimal RetailPrice { get; set; }

        [JsonPropertyName("costPrice")]
        public decimal CostPrice { get; set; }

        [JsonPropertyName("dtcreated")]
        public DateTime dtCreated { get; set; }

        [JsonPropertyName("dtupdated")]
        public DateTime dtUpdated { get; set; }

        [JsonPropertyName("images")]
        public virtual List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();

        [JsonPropertyName("stockAccessories")]
        public virtual List<StockAccessoryViewModel> StockAccessories { get; set; } = new List<StockAccessoryViewModel>();
    }
}
