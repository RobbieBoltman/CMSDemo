using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            try
            {
                var result = await _context.StockItems.FirstOrDefaultAsync(s => s.Id == id);
                if (result == null)
                {
                    //TODO: Log no result
                    return null;
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<StockItem>> ListAllStockItems()
        {
            return await _context.StockItems.ToListAsync();
        }

        public async Task<bool> UpsertStockItem(StockItem stockItem)
        {
            try
            {
                if (stockItem.Id > 0)
                {
                    var existingStockItem = await _context.StockItems
                        .Include(s => s.Images)
                        .Include(s => s.StockAccessories)
                        .FirstOrDefaultAsync(s => s.Id == stockItem.Id);

                    if (existingStockItem == null) 
                        return false;

                    var imagesToRemove = existingStockItem.Images
                        .Where(ei => !stockItem.Images.Any(ni => ni.Id == ei.Id))
                        .ToList();
                    _context.Images.RemoveRange(imagesToRemove);

                    var accessoriesToRemove = existingStockItem.StockAccessories
                        .Where(ea => !stockItem.StockAccessories.Any(na => na.Id == ea.Id))
                        .ToList();
                    _context.StockAccessories.RemoveRange(accessoriesToRemove);

                    var imagesToAdd = stockItem.Images
                        .Where(ni => ni.Id == 0)
                        .ToList();
                    _context.Images.AddRange(imagesToAdd);

                    var accessoriesToAdd = stockItem.StockAccessories
                        .Where(na => na.Id == 0)
                        .ToList();
                    _context.StockAccessories.AddRange(accessoriesToAdd);

                    _context.Entry(existingStockItem).CurrentValues.SetValues(stockItem);
                }
                else                
                    _context.StockItems.Add(stockItem);                

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                return false;
            }
        }


        //public async Task<bool> UpsertStockItem(StockItem stockItem)
        //{
        //    try
        //    {
        //        if (stockItem.Id > 0)
        //        {
        //            var existingStockItemImages = _context.Images.Where(i => i.StockItemId == stockItem.Id).ToList();
        //            var existingStockItemAccessories = _context.StockAccessories.Where(a => a.StockItemId == stockItem.Id).ToList();

        //            _context.StockItems.Update(stockItem);
        //        }
        //        else
        //            _context.StockItems.Add(stockItem);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}
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
