using System;

namespace StockExchange.Domain
{
  public class ActiveStock
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public double PriceTraded { get; set; }
        public double PreviousDayPriceTraded { get; set; }
        public long Volume { get; set; }
        public Company Company { get; set; }
    }
} 