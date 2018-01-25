using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using StockExchange.Auth.Helpers;
using StockExchange.Auth.Helpers.Model;
using StockExchange.Repo;

namespace StockExchange.Service
{
    public class AuthHelper: System.Web.Services.Protocols.SoapHeader
    {
        public bool ValidateToken(string token, out string userName)
        {
            userName = "";
            if (String.IsNullOrEmpty(token))
            {
                return false;
            }
            var configReader = new ConfigurationReader();
            var tokenManager = new TokenManager(configReader);
            var decrypt = AesHelper.Decrypt(token);
            var userTokenData = JsonConvert.DeserializeObject<UserTokenData>(decrypt);
            var isValid =  tokenManager.IsTokenExpired(userTokenData);
            if (isValid)
            {
                userName = StringCipher.Decrypt(userTokenData.CipherUserName, AuthConstants.Password);
            }
            return isValid;
        }
        public object GenerateReFreshToken(string token)
        {
            var configReader = new ConfigurationReader();
            var tokenManager = new TokenManager(configReader);
            var decrypt = AesHelper.Decrypt(token);
            UserTokenData userTokenData = JsonConvert.DeserializeObject<UserTokenData>(decrypt);
            return tokenManager.GenerateUserRefreshToken(userTokenData);
        }
    }
}