using System;
using System.Collections.Generic;

namespace StockManagementAPI.Repositories.DataModels;

public partial class Image
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte[] ImageBinary { get; set; } = null!;

    public int StockItemId { get; set; }

    public virtual StockItem StockItem { get; set; } = null!;
}
