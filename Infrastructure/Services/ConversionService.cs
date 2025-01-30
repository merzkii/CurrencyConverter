using Application.Services;
using Core;
using Core.Exceptions;
using Core.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ConversionService : IConversionService
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IOperationRepository _operationRepository;
        //private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly NbgApiClient _nbgApiClient;
        public ConversionService(IExchangeRateService exchangeRateService, IExchangeRateRepository exchangeRateRepository, NbgApiClient nbgApiClient, IOperationRepository operationRepository)
        {
            _nbgApiClient = nbgApiClient;
            _operationRepository = operationRepository;
            //_exchangeRateRepository = exchangeRateRepository;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<decimal> ConvertCurrencyAsync(
             string clientName,
             string personalNumber,
             string originCurrency,
             string destinationCurrency,
             decimal amount,
             DateTime date
            )
        {

            const string GEL = nameof(GEL);
            var exchangeRates = await _nbgApiClient.FetchExchangeRatesAsync();
            decimal convertedAmount = 0;
            var rate = 0m;
            var targetCurrency = destinationCurrency == GEL ? originCurrency : destinationCurrency;
            if (exchangeRates.Any()&&date.Date==DateTime.Now.Date)
            {
                var exchangeRate = exchangeRates.FirstOrDefault(x => x.Currency == targetCurrency);

                if (exchangeRate != null)
                {

                    convertedAmount = originCurrency == "GEL" ? amount / exchangeRate.Rate : amount * exchangeRate.Rate;
                    rate = exchangeRate.Rate;

                }
                else
                {
                   var convertedCurrency= await CalculateAmountFromDatabase(targetCurrency, originCurrency, amount, date);
                   convertedAmount = convertedCurrency.Amount;
                   rate = convertedCurrency.Rate;
                    
                }

            }
            //var rate = await _exchangeRateService.GetExchangeRateAsync(destinationCurrency, date);
            //if (rate == null)
            //throw new NotFoundException("Exchange rate not found.");
            //return amount / rate.Value;
            else
            {
                var convertedCurrency = await CalculateAmountFromDatabase(targetCurrency, originCurrency, amount, date);
                convertedAmount = convertedCurrency.Amount;
                rate = convertedCurrency.Rate;
            }
            var operation = new Operation
            {
                ClientName = clientName,
                PersonalNumber = personalNumber,
                FromCurrency = originCurrency,
                ToCurrency = destinationCurrency,
                ConvertedAmount = convertedAmount,
                Amount = amount,
                Rate = rate,
                Date = DateTime.UtcNow
            };

            await _operationRepository.AddOperationAsync(operation);

            return convertedAmount;
        }

        private async Task<ConvertedCurrency> CalculateAmountFromDatabase(string targetCurrency,string originCurrency,decimal amount, DateTime date)
        {
            var storedRate = await _exchangeRateService.GetExchangeRateAsync(targetCurrency, date);
            if (storedRate == null)
                throw new Exception("Exchange rate not found in database.");
            var convertedAmount = originCurrency == "GEL" ? amount / storedRate : amount * storedRate;
            var rate = storedRate;
            var convertedCurrency = new ConvertedCurrency(convertedAmount, rate);
            return convertedCurrency;
        }
    }

    public class ConvertedCurrency
    {
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public ConvertedCurrency(decimal amount,decimal rate)
        {
            Amount = amount;
            Rate = rate;
        }
    }

}
