using System;
using System.Collections.Generic;

namespace StockManagementAPI.Models;

public partial class StockItem
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

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<StockAccessory> StockAccessories { get; set; } = new List<StockAccessory>();
}
