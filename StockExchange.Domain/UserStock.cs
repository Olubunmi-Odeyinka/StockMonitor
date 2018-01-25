using System;

namespace StockExchange.Domain
{
  public class UserStock
    {
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string UserName { get; set; }
    public Company Company { get; set; }
    }
} 