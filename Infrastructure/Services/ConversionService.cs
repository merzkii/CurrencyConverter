using Core.Exceptions;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
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
            if (originCurrency == "GEL")
            {
                if (exchangeRates.Any())
                {
                    var rate=exchangeRates.First(x => x.Currency == destinationCurrency).Rate;
                    return amount / rate;
                }
                else
                {
                    var rate= await _exchangeRateService.GetExchangeRateAsync(destinationCurrency, date);
                    return amount / rate;
                }

                //var rate = await _exchangeRateService.GetExchangeRateAsync(destinationCurrency, date);
                //if (rate == null)
                    //throw new NotFoundException("Exchange rate not found.");
                //return amount / rate.Value;
            }
            else if (destinationCurrency == "GEL")
            {
                if (exchangeRates.Any())
                {
                    var rate = exchangeRates.First(x => x.Currency == destinationCurrency).Rate;
                    return amount * rate;
                }
                else
                {
                    var rate = await _exchangeRateService.GetExchangeRateAsync(destinationCurrency, date);
                    return amount * rate;
                }
            }
            else
            {
                throw new ValidationException("Cross-currency conversion is not supported.");
            }
        }
    }
}
