using Application.Services;
using CurrencyConverter.Web.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    public class ConversionController : Controller
    {
        private readonly IConversionService _conversionService;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly IOperationRepository _operationRepository;
        private readonly NbgApiClient _bgApiClient;

        public ConversionController(IOperationRepository operationRepository, IConversionService conversionService,IExchangeRateService exchangeRateService,NbgApiClient nbgApiClient, IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
            _bgApiClient = nbgApiClient;
            _conversionService = conversionService;
            _exchangeRateService = exchangeRateService;
            _operationRepository = operationRepository; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var client=new HttpClient();    
            //var rates = new NbgApiClient();
            //rates.FetchExchangeRatesAsync().Wait();

            var exchangeRatesTask = _bgApiClient.FetchExchangeRatesAsync();
            exchangeRatesTask.Wait();
            var exchangeRates = exchangeRatesTask.Result;
            await _exchangeRateRepository.AddRateAsync(exchangeRates);
            var operations=await _operationRepository.GetOperationsAsync();
            var model = new ConversionViewModel
            {
                Rate = _bgApiClient,
                Operations= (List<Core.Models.Operation>)operations
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Convert(string clientName, string personalNumber, string originCurrency, string destinationCurrency, decimal amount, DateTime? date)
        {
            DateTime conversionDate = date ?? DateTime.Now;
            var convertedAmount = await _conversionService.ConvertCurrencyAsync(clientName,personalNumber, originCurrency, destinationCurrency, amount,conversionDate);
            var operations = await _operationRepository.GetOperationsAsync();
            var model = new ConversionViewModel
            {
                ClientName = clientName,
                PersonalNumber = personalNumber,
                OriginCurrency = originCurrency,
                DestinationCurrency = destinationCurrency,
                Amount = amount,
                Date = conversionDate,
                ConvertedAmount = convertedAmount,
                Operations= (List<Core.Models.Operation>)operations
                
            };
            return View("index",model);
        }
    }
}
