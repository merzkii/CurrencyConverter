using Application.Services;
using CurrencyConverter.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    public class ConversionController : Controller
    {
        private readonly IConversionService _conversionService;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly NbgApiClient _bgApiClient;

        public ConversionController(IConversionService conversionService,IExchangeRateService exchangeRateService,NbgApiClient nbgApiClient)
        {
            _bgApiClient = nbgApiClient;
            _conversionService = conversionService;
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string currencyCode,DateTime date)
        {
            //var client=new HttpClient();    
            //var rates = new NbgApiClient();
            //rates.FetchExchangeRatesAsync().Wait();
            _bgApiClient.FetchExchangeRatesAsync().Wait();
            var model = new ConversionViewModel
            {
                Rate = _bgApiClient
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Convert(string originCurrency, string destinationCurrency, decimal amount,DateTime date)
        {

            var convertedAmount = await _conversionService.ConvertCurrencyAsync(originCurrency, destinationCurrency, amount,date);

            ViewBag.ConvertedAmount = convertedAmount;
            return View("index");
        }
    }
}
