using System;
using System.Collections.Generic;

namespace StockExchange.Domain
{
  public class Company
  {
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TicketSymbol { get; set; }
    public List<UserStock> UserStocks { get; set; }
    }
} 