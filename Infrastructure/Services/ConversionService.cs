using Application.Services;
using Core;
using Core.Exceptions;
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
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public ConversionService(IExchangeRateService exchangeRateService,IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository= exchangeRateRepository;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<decimal> ConvertCurrencyAsync(
            string originCurrency,
            string destinationCurrency,
            decimal amount,
            DateTime date)
        {
            var outSource = new NbgApiClient();
            var exchangeRates = await outSource.FetchExchangeRatesAsync();
            if (exchangeRates.Any())
            {
                // Find the rate for the requested currency
                var exchangeRate = exchangeRates.FirstOrDefault(x => x.Currency == destinationCurrency);

                if (exchangeRate != null)
                {
                    // Save exchange rate in database if not already stored
                    await _exchangeRateRepository.AddRateAsync(new ExchangeRate
                    {
                        Currency = exchangeRate.Currency,
                        Rate = exchangeRate.Rate,
                        Date = date
                    });

                    return originCurrency == "GEL" ? amount / exchangeRate.Rate : amount * exchangeRate.Rate;
                }
            }
            //var rate = await _exchangeRateService.GetExchangeRateAsync(destinationCurrency, date);
            //if (rate == null)
            //throw new NotFoundException("Exchange rate not found.");
            //return amount / rate.Value;

            var storedRate = await _exchangeRateService.GetExchangeRateAsync(destinationCurrency, date);
            if (storedRate == null)
                throw new Exception("Exchange rate not found in database.");

            return originCurrency == "GEL" ? amount / storedRate : amount * storedRate;
        }
    }
    
}
