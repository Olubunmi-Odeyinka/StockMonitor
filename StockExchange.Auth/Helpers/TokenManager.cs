using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockExchange.Auth.Helpers.Model;
using StockExchange.Auth.Interfaces;

namespace StockExchange.Auth.Helpers
{
	public class TokenManager
	{
		private readonly IConfigurationReader _configurationReader;

		public TokenManager(IConfigurationReader configurationReader)
		{
			_configurationReader = configurationReader;
		}

		#region IsTokenExpired

		//This check the time data in the to see if the token has expire already
		public bool IsTokenExpired(UserTokenData userTokenData)
		{
		    if (userTokenData == null)
		    {
		        return true;
		    }

			var now = DateTime.Now;

			if ((DateTime.Compare(userTokenData.AbsoluteExpiration, now) < 0)
			|| (DateTime.Compare(userTokenData.SlidingExpiration, now) < 0))
			{
				return false;
			}
			return true;
		}

		#endregion

		#region GenerateUserRefreshToken
		//This generate a refresh token for the user by updating the sliding expiration field of the token
		public string GenerateUserRefreshToken(UserTokenData userTokenData)
		{
			int periodSliding = _configurationReader.GetSlidingExpiration();

			var freshUserTokenData = new UserTokenData();
			var now = DateTime.Now;

			freshUserTokenData.SlidingExpiration = now.AddMinutes(periodSliding);
			freshUserTokenData.AbsoluteExpiration = userTokenData.AbsoluteExpiration;
		    freshUserTokenData.PresentTime = DateTime.Now;
			freshUserTokenData.CipherUserName = userTokenData.CipherUserName;

			var newToken = AesHelper
							.Encrypt(
							JsonConvert
							.SerializeObject(freshUserTokenData));

			return newToken;
		}

		#endregion

		#region GenerateNewTokenWithExpiryData

		public string GenerateNewTokenWithExpiryData(string userNameShaEncryptedString)
		{
			var periodSliding = _configurationReader.GetSlidingExpiration();
			var periodAbsolute = _configurationReader.GetAbsoluteExpiration();

			var userTokenData = new UserTokenData();
			var now = DateTime.Now;

			userTokenData.SlidingExpiration = now.AddMinutes(periodSliding);
            userTokenData.PresentTime = DateTime.Now;
            userTokenData.AbsoluteExpiration = now.AddHours(periodAbsolute);
			userTokenData.CipherUserName = userNameShaEncryptedString;

			var newToken = AesHelper
							.Encrypt(
							JsonConvert
							.SerializeObject(
							userTokenData));

			return newToken;
		}

		#endregion
	}
}