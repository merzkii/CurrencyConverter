using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IExchangeRateRepository
    {
        Task<ExchangeRate?> GetRateAsync(string currency, DateTime date);
        Task AddRateAsync(ExchangeRate rate);
    }
}

