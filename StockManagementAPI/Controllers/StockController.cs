using Microsoft.AspNetCore.Mvc;
using StockManagementAPI.Repositories;
using StockManagementAPI.ViewModels;

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

        [HttpGet]
        public async Task<List<StockItemDashboardViewModel>> ListAllStockItemsDashboard()
        {
            var allStock = await _stockRepository.ListAllStockItems();

            var allStockDashboardViewModel = new List<StockItemDashboardViewModel>();

            allStock.ForEach(s => allStockDashboardViewModel.Add(new StockItemDashboardViewModel()
            {
                Id = s.Id,
                Dtcreated = s.Dtcreated,
                Images = s.Images.ToList(),
                Kms = s.Kms,
                Make = s.Make,
                Model = s.Model,
                ModelYear = s.ModelYear,
                RetailPrice = s.RetailPrice
            }));

            return allStockDashboardViewModel;
        }
    }
}
