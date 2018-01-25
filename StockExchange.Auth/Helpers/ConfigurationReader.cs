using System;
using System.Configuration;
using StockExchange.Auth.Interfaces;

namespace StockExchange.Auth.Helpers
{
	public class ConfigurationReader : IConfigurationReader
	{
		#region Common

		public int GetSlidingExpiration()
		{
			var slidingExpirationString = ConfigurationManager.AppSettings["SlidingExpiration"];
			if (!String.IsNullOrEmpty(slidingExpirationString))
			{
				return int.Parse(slidingExpirationString);
			}
			return 30;
		}

		public int GetAbsoluteExpiration()
		{
			var absoluteExpiration = ConfigurationManager.AppSettings["AbsoluteExpiration"];
			if (!String.IsNullOrEmpty(absoluteExpiration))
			{
				return int.Parse(absoluteExpiration);
			}
			return 24;
		}

		#endregion
	}
}