using Microsoft.EntityFrameworkCore;
using StockManagementAPI.Repositories.DataModels;

namespace StockManagementAPI.Repositories
{
    public class StockRepository : IStockRepository
    {
        private CmsdemoContext _context;

        public StockRepository(CmsdemoContext context)
        {
            _context = context;
        }

        public async Task<StockItem> GetStockItemById(int id)
        {
            return await _context.StockItems.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<StockItem>> ListAllStockItems()
        {
            return await _context.StockItems.ToListAsync();
        }

        public async Task<bool> UpsertStockItem(StockItem stockItem)
        {
            try
            {
                var existingStockItem = await _context.StockItems.FirstOrDefaultAsync(s => stockItem.Id == stockItem.Id);
                _context.StockItems.Update(stockItem);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteStockItemById(int id)
        {
            try
            {
                var existingStockItem = await _context.StockItems.FirstOrDefaultAsync(s => id == s.Id);
                
                if (existingStockItem == null)
                    return false;

                _context.StockItems.Remove(existingStockItem);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Image>> ListAllImagesByStockItemId(int stockItemId)
        {
            return await _context.Images.Where(i => i.StockItemId == stockItemId).ToListAsync();
        }

        public async Task<List<StockAccessory>> ListAllAccessoriesByStockItemId(int stockItemId)
        {
            return await _context.StockAccessories.Where(a => a.StockItemId == stockItemId).ToListAsync();
        }
    }
}
