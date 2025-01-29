using Infrastructure.Repositories;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Application.Services;

namespace Infrastructure.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRateRepository _repository;

        public ExchangeRateService(IExchangeRateRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> GetExchangeRateAsync(string currencyCode, DateTime date)
        {
            var exchangeRate=await _repository.GetRateAsync(currencyCode, date);

            if (exchangeRate == null)
            {
                throw new NotFoundException("No exchange rate available for the given date.");
            }
            return exchangeRate.Rate;
        }
    }
}
