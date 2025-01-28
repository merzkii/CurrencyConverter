using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string PersonalNumber { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
