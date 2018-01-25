using System.ComponentModel.DataAnnotations;

namespace StockExchange.Domain.ViewModels
{
    public class ActiveStockViewModel
    {
        public int Id { get; set; }
        public string TicketSymbol { get; set; }
        public double PriceTraded { get; set; }
        public double PreviousDayPriceTraded { get; set; }
        public double RateChange { get; set; }
        public long Volume { get; set; }
    }
} 