using StockExchange.Repo.Enums;

namespace StockExchange.Repo.Interfaces
{
  public interface IStateObject
  {
    ObjectState State { get; }
  }
}