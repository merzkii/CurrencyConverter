using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Web.Models
{
    public class ConversionViewModel
    {
        [Required]
        [Display(Name = "Client Name")]
        [RegularExpression(@"^\w+\s\w+$", ErrorMessage = "Client name must contain exactly two words.")]
        public string ClientName { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "Personal number must be 11 digits.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Personal number must contain only numeric values.")]
        [Display(Name = "Personal Number")]
        public string PersonalNumber { get; set; }

        [Required]
        [Display(Name = "Origin Currency")]
        public string OriginCurrency { get; set; }

        [Required]
        [Display(Name = "Destination Currency")]
        public string DestinationCurrency { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public decimal ConvertedAmount { get; set; }
        public DateTime ExchangeRateDate { get; set; }

        [Display(Name = "Rates")]
        public NbgApiClient Rate { get; set; }

        public List<Operation> Operations { get; set; }
    }
}
