using System.ComponentModel.DataAnnotations;

namespace StockExchange.Domain.ViewModels
{
    public class UserStockViewModel
    {
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Ticket Symbol")]
        public string TicketSymbol { get; set; }
        [Required]
        public bool IsAdded { get; set; } = false;
    }
} 