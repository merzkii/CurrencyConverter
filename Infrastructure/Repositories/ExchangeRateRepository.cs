using Core;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly AppDbContext _context;

        public ExchangeRateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExchangeRate?> GetRateAsync(string currency, DateTime date)
        {
           
            return await _context.ExchangeRates
                .Where(r => r.Currency == currency && r.Date <= date)
                .OrderByDescending(r => r.Date)
                .FirstOrDefaultAsync();
        }

        public async Task AddRateAsync(IEnumerable<ExchangeRate> exchangeRates)
        {

            foreach (var rate in exchangeRates)
            {
                var existingRate = await _context.ExchangeRates
                    .FirstOrDefaultAsync(r => r.Currency == rate.Currency && r.Date == rate.Date);

                if (existingRate == null)
                {
                    _context.ExchangeRates.Add(rate);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
