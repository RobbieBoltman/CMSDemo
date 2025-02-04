using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockManagementAPI.Repositories.DataModels;
using System.Net.WebSockets;

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
                //TODO: Log exception
                return null;
            }
        }

        public async Task<List<StockItem>> ListAllStockItems()
        {
            var result = await _context.StockItems
                .Include(si => si.Images)
                .ToListAsync();

            return result;
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

                    var accessoriesToRemove = existingStockItem.StockAccessories
                        .Where(ea => !stockItem.StockAccessories
                            .Any(na => na.Id == ea.Id))
                        .ToList();
                    _context.StockAccessories.RemoveRange(accessoriesToRemove);

                    foreach (var updatedAccessory in stockItem.StockAccessories)
                    {
                        if (updatedAccessory.Id != 0)
                        {
                            var existingAccessory = existingStockItem.StockAccessories
                                .FirstOrDefault(ea => ea.Id == updatedAccessory.Id);

                            if (existingAccessory != null && existingAccessory.Description != updatedAccessory.Description)
                            {                                
                                existingAccessory.Description = updatedAccessory.Description;
                                _context.StockAccessories.Update(existingAccessory);
                            }
                        }
                    }

                    var newAccessories = stockItem.StockAccessories
                        .Where(na => na.Id == 0)
                        .ToList();

                    foreach (var newAccessory in newAccessories)                    
                        _context.StockAccessories.Add(newAccessory);

                    var imagesToRemove = existingStockItem.Images
                        .Where(ei => !stockItem.Images.Any(ni => ni.Id == ei.Id))
                        .ToList();
                    _context.Images.RemoveRange(imagesToRemove);

                    var imagesToAdd = stockItem.Images
                        .Where(ni => ni.Id == 0)
                        .ToList();
                    _context.Images.AddRange(imagesToAdd);

                    _context.Entry(existingStockItem).CurrentValues.SetValues(stockItem);
                }
                else                
                    _context.StockItems.Add(stockItem);                

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // TODO: Add logging
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
