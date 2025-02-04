using Microsoft.AspNetCore.Mvc;
using StockManagementAPI.Repositories;
using StockManagementAPI.Repositories.DataModels;
using StockManagementAPI.ViewModels;
using System.Collections.ObjectModel;

namespace StockManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private IStockRepository _stockRepository;

        public StockController(ILogger<StockController> logger, IStockRepository stockRepository)
        {
            _logger = logger;
            _stockRepository = stockRepository;
        }

        [HttpGet("ListAllStockItemsDashboard")]
        public async Task<List<StockItemDashboardViewModel>> ListAllStockItemsDashboard()
        {
            var allStock = await _stockRepository.ListAllStockItems();

            var allStockDashboardViewModel = new List<StockItemDashboardViewModel>();

            allStock.ForEach(s => allStockDashboardViewModel.Add(new StockItemDashboardViewModel()
            {
                Id = s.Id,
                dtCreated = s.dtCreated,
                Images = s.Images.Select(i => new ImageViewModel()
                {
                    Id = i.Id,
                    ImageBinary = i.ImageBinary,
                    Name = i.Name
                }).ToList(),
                Kms = s.Kms,
                Make = s.Make,
                Model = s.Model,
                ModelYear = s.ModelYear,
                RetailPrice = s.RetailPrice
            }));

            return allStockDashboardViewModel;
        }
        [HttpGet("GetStockItemById/{id}")]
        public async Task<StockItemViewModel> GetStockItemById(int id)
        {
            var stockItem = await _stockRepository.GetStockItemById(id);

            return new StockItemViewModel()
            {
                Id = stockItem.Id,
                RegNo = stockItem.RegNo,
                Make = stockItem.Make,
                Model = stockItem.Model,
                ModelYear = stockItem.ModelYear,
                Kms = stockItem.Kms,
                Colour = stockItem.Colour,
                Vin = stockItem.Vin,
                RetailPrice = stockItem.RetailPrice,
                CostPrice = stockItem.CostPrice,
                dtCreated = stockItem.dtCreated,
                dtUpdated = stockItem.dtUpdated,

                Images = (await _stockRepository.ListAllImagesByStockItemId(id))
                         ?.Select(i => new ImageViewModel
                         {
                             Id = i.Id,
                             Name = i.Name,
                             ImageBinary = i.ImageBinary
                         }).ToList() ?? new List<ImageViewModel>(),

                StockAccessories = (await _stockRepository.ListAllAccessoriesByStockItemId(id))
                                   ?.Select(a => new StockAccessoryViewModel
                                   {
                                       Id = a.Id,
                                       Description = a.Description
                                   }).ToList() ?? new List<StockAccessoryViewModel>()
            };
        }

        [HttpPost("UpsertStockItem")]
        public async Task<bool> UpsertStockItem(StockItemViewModel stockItem)
        {
            return await _stockRepository.UpsertStockItem(new StockItem()
            {
                Id = stockItem.Id,
                RegNo = stockItem.RegNo,
                Make = stockItem.Make,
                Model = stockItem.Model,
                ModelYear = stockItem.ModelYear,
                Kms = stockItem.Kms,
                Colour = stockItem.Colour,
                Vin = stockItem.Vin,
                RetailPrice = stockItem.RetailPrice,
                CostPrice = stockItem.CostPrice,
                dtCreated = stockItem.dtCreated,
                dtUpdated = DateTime.Now,
                Images = stockItem.Images?.Select(i => new Image
                {
                    Id = i.Id,
                    Name = i.Name,
                    ImageBinary = i.ImageBinary,
                    StockItemId = stockItem.Id
                }).ToList() ?? new List<Image>(),
                StockAccessories = stockItem.StockAccessories?.Select(i => new StockAccessory
                {
                    Id = i.Id,
                    Description = i.Description,
                    StockItemId = stockItem.Id
                }).ToList() ?? new List<StockAccessory>()
            });
        }

        [HttpDelete]
        public async Task<bool> DeleteStockItem(int id)
        {
            return await _stockRepository.DeleteStockItemById(id);
        }
    }
}
