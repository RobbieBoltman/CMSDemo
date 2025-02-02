using System;
using System.Collections.Generic;

namespace StockManagementAPI.Repositories.DataModels;

public partial class StockAccessory
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int StockItemId { get; set; }

    public virtual StockItem StockItem { get; set; } = null!;
}
