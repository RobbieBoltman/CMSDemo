using StockManagementAPI.Repositories.DataModels;

namespace StockManagementAPI.Repositories
{
    public interface IStockRepository
    {
        Task<List<StockItem>> ListAllStockItems();
        Task<StockItem> GetStockItemById(int id);
        Task<bool> UpsertStockItem(StockItem stockItem);
        Task<bool> DeleteStockItemById(int id);
        Task<List<Image>> ListAllImagesByStockItemId(int stockItemId);
        Task<List<StockAccessory>> ListAllAccessoriesByStockItemId(int stockItemId);
    }
}
