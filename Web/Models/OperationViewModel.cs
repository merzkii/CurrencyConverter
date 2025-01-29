namespace CurrencyConverter.Web.Models
{
    public class OperationViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string PersonalNumber { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
