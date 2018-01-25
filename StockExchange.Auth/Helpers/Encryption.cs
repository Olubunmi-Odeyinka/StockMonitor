using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace StockExchange.Auth.Helpers
{
	public static class Encryption
	{
		# region AuthConstants

		public const string DefaultKey = AuthConstants.Sha512Key;

		# endregion

		# region SHA512

		public static string ToSha512(this string input, string key = DefaultKey)
		{
			return input.Trim().ToLower().Hash<HMACSHA512>(key);
		}

		public static string ToSha512(this Guid input, string key = DefaultKey)
		{
			return input.ToUpperString().ToSha512(key);
		}

		# endregion

		# region ToHexString

		public static string ToHexString(this IEnumerable<byte> bytes)
		{
			var result = new StringBuilder();
			foreach (var b in bytes)
			{
				result.Append(b.ToString("X2"));
			}
			return result.ToString();
		}

		# endregion

		# region ToUpperString

		public static string ToUpperString(this Guid input)
		{
			return input.ToString("N").ToUpper();
		}

		# endregion

		# region Private Methods

		private static string Hash<T>(this string input, string key) where T : HMAC, new()
		{
			var provider = new T();
			if (key != null)
			{
				provider.Key = Encoding.UTF8.GetBytes(key);
			}

			var bytes = provider.ComputeHash(Encoding.UTF8.GetBytes(input));
			return bytes.ToHexString();
		}

		# endregion
	}

}