using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StockExchange.Auth.Helpers
{
	public class AesHelper
	{

		#region Settings

		private const int Iterations = 2;
		private const int KeySize = 256;
				
		private const string Hash = "SHA1";
		private const string Salt = AuthConstants.Salt; // Random 
		private const string Vector = AuthConstants.Vector; // Random
		private const string Password = AuthConstants.Password;

		#endregion

		#region Aes Encrypt

		public static string Encrypt(string password)
		{
			return Encrypt<AesManaged>(password);
		}
		public static string Encrypt<T>(string password)
				where T : SymmetricAlgorithm, new()
		{
			var ascii = Encoding.ASCII;
			var utc = Encoding.UTF8;
			byte[] vectorBytes = ascii.GetBytes(Vector);
			byte[] saltBytes = ascii.GetBytes(Salt);
			byte[] valueBytes = utc.GetBytes(password);

			byte[] encrypted;
			using (T cipher = new T())
			{
				PasswordDeriveBytes passwordBytes =
					new PasswordDeriveBytes(Password
											, saltBytes
											, Hash
											, Iterations);
				byte[] keyBytes = passwordBytes.GetBytes(KeySize / 8);

				cipher.Mode = CipherMode.CBC;

				using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
				{
					using (MemoryStream to = new MemoryStream())
					{
						using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
						{
							writer.Write(valueBytes, 0, valueBytes.Length);
							writer.FlushFinalBlock();
							encrypted = to.ToArray();
						}
					}
				}
				cipher.Clear();
			}
			return Convert.ToBase64String(encrypted);
		}

		#endregion

		#region Aes Decrypt

		public static string Decrypt(string password)
		{
			return Decrypt<AesManaged>(password);
		}
		public static string Decrypt<T>(string password) where T : SymmetricAlgorithm, new()
		{
			var ascii = Encoding.ASCII;
			byte[] vectorBytes = ascii.GetBytes(Vector);
			byte[] saltBytes = ascii.GetBytes(Salt);
			byte[] valueBytes = Convert.FromBase64String(password);

			byte[] decrypted;
			int decryptedByteCount = 0;

			using (T cipher = new T())
			{
				PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(Password
																			,saltBytes
																			, Hash
																			, Iterations);
				byte[] keyBytes = passwordBytes.GetBytes(KeySize / 8);

				cipher.Mode = CipherMode.CBC;

				try
				{
					using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
					{
						using (MemoryStream from = new MemoryStream(valueBytes))
						{
							using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
							{
								decrypted = new byte[valueBytes.Length];
								decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
							}
						}
					}
				}
				catch (Exception ex)
				{
					return String.Empty;
				}

				cipher.Clear();
			}
			return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
		}

		#endregion
	}
}