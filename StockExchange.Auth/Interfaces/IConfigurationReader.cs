namespace StockExchange.Auth.Interfaces
{
	public interface IConfigurationReader
	{
		#region Common

		int GetSlidingExpiration();
		int GetAbsoluteExpiration();

		#endregion
	}
}
