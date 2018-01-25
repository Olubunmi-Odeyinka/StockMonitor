using System;

namespace StockExchange.Auth.Helpers.Model
{
	public class UserTokenData
	{
		public string CipherUserName { get; set; }
		public DateTime AbsoluteExpiration { get; set; }
		public DateTime SlidingExpiration { get; set; }
        public DateTime PresentTime { get; set; }
	}
}