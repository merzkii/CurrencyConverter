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

        public async Task AddRateAsync(ExchangeRate rate)
        {
            await _context.ExchangeRates.AddAsync(rate);
            await _context.SaveChangesAsync();
        }
    }
}
